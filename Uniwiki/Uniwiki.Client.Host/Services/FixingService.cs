using System;
using System.Threading.Tasks;
using Blazored.Toast.Services;
using Shared.Dtos;
using Uniwiki.Client.Host.Services.Abstractions;

namespace Uniwiki.Client.Host.Services
{
    internal class FixingService : IFixingService
    {
        private readonly LocalLoginService _localLoginService;
        private readonly IToastService _toastService;
        private readonly TextService _textService;
        private readonly INavigationService _navigationService;

        public FixingService(LocalLoginService localLoginService, IToastService toastService, TextService textService, INavigationService navigationService)
        {
            _localLoginService = localLoginService;
            _toastService = toastService;
            _textService = textService;
            _navigationService = navigationService;
        }

        public async Task Fix(ErrorFix errorFix)
        {
            switch (errorFix)
            {
                case ErrorFix.Logout:
                    if (_localLoginService.IsAuthenticated)
                    {
                        await _localLoginService.LocalLogout();
                        _toastService.ShowInfo(_textService.Toast_AppDidAutomaticLogout, _textService.Toast_Info);
                    }
                    break;
                case ErrorFix.WrongVersion:
                    _navigationService.ForceReload();
                    Console.WriteLine("After Fix of versions");
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(errorFix), errorFix, null);
            }
        }
    }
}