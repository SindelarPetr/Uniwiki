using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Blazored.Toast.Services;
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

        private async Task<T> SendRequestInnerAsync<T>(RequestBase<T> request, Func<string, HttpContent> dataCreator, string apiPath, Action? finalAction = null, string callerName = null, int lineNumber = 0) where T : ResponseBase
        {
            try
            {
                // Serialize the request
                var serialized = await Task.Run(() => JsonConvert.SerializeObject(request));

                // Get the type of the request
                var type = request.GetType();

                // Wrap the request to DataForServer
                var wrapped = new DataForServer(serialized, type, _localAuthenticationStateProvider.Token, _textService.Language, ClientConstants.AppVersionString);

                // Serialize the wrapper
                var serializedWrapped = JsonSerializer.Serialize(wrapped);

                // Create HTTP request
                var data = dataCreator.Invoke(serializedWrapped);

                // Call the server API
                var httpResponse = await _client.PostAsync(apiPath, data);

                // Throw if it was not successful
                httpResponse.EnsureSuccessStatusCode();

                // Get the serialized response
                var serializedResponse = await httpResponse.Content.ReadAsStringAsync();

                // Deserialize the response
                var deserializedResponse = JsonConvert.DeserializeObject<DataForClient<T>>(serializedResponse);

                // Fix fixable errors
                if (deserializedResponse.Fixes != null)
                    foreach (var fix in deserializedResponse.Fixes)
                    {
                        await _fixingService.Fix(fix.ErrorFix);
                    }

                // Display the received error and throw an exception
                if (deserializedResponse.Error != null)
                {
                    _toastService.ShowError(deserializedResponse.Error.Message, _textService.Toast_Error);
                    throw new RequestRejectedException(deserializedResponse.Error.Message);
                }

                if (deserializedResponse.Response == null)
                {
                    var message = "There is a bug on the server! Didnt get any response nor error back :(.";
                    _toastService.ShowError(message, _textService.Toast_Error);
                    throw new RequestRejectedException(message);
                }

                return deserializedResponse.Response;
            }
            catch (HttpRequestException ex)
            {
                _toastService.ShowError(_textService.Error_ErrorOnServer, _textService.Toast_Error);
                throw new ServerErrorException();
            }
            catch (JsonException ex) // Could not parse the response
            {
                _toastService.ShowError(_textService.Error_ErrorOnServer, _textService.Toast_Error);
                throw new ServerErrorException();
            }
            catch (Exception ex) // Connection error
            {
                if (ex is RequestRejectedException requestRejectedException)
                    throw requestRejectedException;

                var message = ex.Message;
                _toastService.ShowError(_textService.Error_ConnectionError, _textService.Toast_Error);
                Console.WriteLine(ex);
                throw new ConnectionException();
            }
            finally
            {
                finalAction?.Invoke();
            }
        }
    }
}











