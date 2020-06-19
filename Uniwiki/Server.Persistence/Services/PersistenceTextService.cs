using Uniwiki.Shared.Services;

namespace Server.Persistence.Services
{
    public class PersistenceTextService
    {
        private readonly TextServiceBase _textServiceBase;

        public PersistenceTextService(TextServiceBase textServiceBase)
        {
            _textServiceBase = textServiceBase;
        }


    }
}
