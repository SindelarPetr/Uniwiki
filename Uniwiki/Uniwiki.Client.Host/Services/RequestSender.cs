using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Blazored.Toast.Services;
using BlazorInputFile;
using Newtonsoft.Json;
using Shared.Dtos;
using Shared.RequestResponse;
using Uniwiki.Client.Host.Exceptions;
using Uniwiki.Client.Host.Services.Abstractions;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace Uniwiki.Client.Host.Services
{
    internal class RequestSender : IRequestSender
    {
        private readonly IHttpService _client;
        private readonly ILocalAuthenticationStateProvider _localAuthenticationStateProvider;
        private readonly IToastService _toastService;
        private readonly TextService _textService;
        private readonly IFixingService _fixingService;

        public RequestSender(IHttpService client, ILocalAuthenticationStateProvider localAuthenticationStateProvider, IToastService toastService, TextService textService, IFixingService fixingService)
        {
            _client = client;
            _localAuthenticationStateProvider = localAuthenticationStateProvider;
            _toastService = toastService;
            _textService = textService;
            _fixingService = fixingService;
        }

        public Task<T> SendRequestAsync<T>(RequestBase<T> request, Action? finalAction = null, [CallerMemberName] string callerName = null, [CallerLineNumber] int lineNumber = 0) where T : ResponseBase
        {
            var dataCreator = new Func<string, HttpContent>(serializedData => new StringContent(serializedData, Encoding.UTF8, "application/json"));

            return SendRequestInnerAsync(request, dataCreator, "api/server", finalAction, callerName, lineNumber);
        }

        public async Task<T> SendRequestAsync<T>(RequestBase<T> request, IFileListEntry file, Action? finalAction = null,
            string callerName = "", int lineNumber = 0) where T : ResponseBase
        {

            byte[] fileBytes;
            try
            {
                await using var ms = new MemoryStream();
                await file.Data.CopyToAsync(ms);
                fileBytes = ms.GetBuffer();
            }
            catch (Exception e)
            {
                _toastService.ShowError(_textService.Toast_ErrorWhileUploadingFile(file.Name), _textService.Toast_Error);
                throw;
            }

            var dataCreator = new Func<string, HttpContent>(
                serializedData => new MultipartFormDataContent {
                        { new StringContent(serializedData, Encoding.UTF8, "application/json"),"\"Data\"" },
                        { new ByteArrayContent(fileBytes), "\"upload\"", file.Name }
                    });

            return await SendRequestInnerAsync(request, dataCreator, "upload", finalAction, callerName, lineNumber);
        }

        private async Task<T> SendRequestInnerAsync<T>(RequestBase<T> request, Func<string, HttpContent> dataCreator, string apiPath, Action? finalAction = null, string callerName = null, int lineNumber = 0) where T : ResponseBase
        {
            try
            {
                //Console.WriteLine("// Serializing request from: " + callerName + " on line " + lineNumber);
                var serialized = await Task.Run(() => JsonConvert.SerializeObject(request));
                //Console.WriteLine(serialized);

                // Get the type of the request
                var type = request.GetType();

                var wrapped = new DataForServer(serialized, type, _localAuthenticationStateProvider.Token, _textService.Language, ClientConstants.AppVersionString);
                //Console.WriteLine("Wrapped serialized request");

                var serializedWrapped = JsonSerializer.Serialize(wrapped);
                //Console.WriteLine("Serialized wrapped serialized request");

                var data = dataCreator.Invoke(serializedWrapped);

                HttpResponseMessage httpResponse;

                try
                {
                    //Console.WriteLine("Before sending");
                    httpResponse = await _client.PostAsync(apiPath, data);
                    //Console.WriteLine("After sending");
                }
                catch (HttpRequestException ex)
                {
                    var message = ex.Message;
                    _toastService.ShowError(_textService.Error_ConnectionError, _textService.Toast_Error);
                    throw new ConnectionException();
                }

                if (!httpResponse.IsSuccessStatusCode && (int)httpResponse.StatusCode >= 500 && (int)httpResponse.StatusCode <= 599)
                {
                    _toastService.ShowError(_textService.Error_ErrorOnServer, _textService.Toast_Error);
                    throw new ServerErrorException();
                }

                //Console.WriteLine("// Reading response");
                var responseString = await httpResponse.Content.ReadAsStringAsync();
                //Console.WriteLine(responseString);

                //Console.WriteLine("// Deserializing response to data for client");
                var dataForClient = JsonConvert.DeserializeObject<DataForClient<T>>(responseString);

                // Fix fixable errors
                if (dataForClient.Fixes != null)
                    foreach (var fix in dataForClient.Fixes)
                    {
                        //Console.WriteLine("Response has fix: " + fix.Message);

                        await _fixingService.Fix(fix.ErrorFix);
                    }

                // Display the received error and throw an exception
                if (dataForClient.Error != null)
                {
                    _toastService.ShowError(dataForClient.Error.Message, _textService.Toast_Error);
                    throw new RequestRejectedException(dataForClient.Error.Message);
                }

                if (dataForClient.Response == null)
                {
                    var message = "There is a bug on the server! Didnt get any response nor error back :(.";
                    _toastService.ShowError(message, _textService.Toast_Error);
                    throw new RequestRejectedException(message);
                }

                return dataForClient.Response;
            }
            finally
            {
                finalAction?.Invoke();
            }
        }
    }
}











