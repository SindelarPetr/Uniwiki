using System;

namespace Shared.Services
{
    public interface IStringStandardizationService
    { 
        string CreateUrl(string text, Func<string, bool> isUniq);
        string OptimizeWhiteSpaces(string? text);
        string StandardizeSearchText(string text);
        string RemoveAccents(string text);
        string StandardizeEmail(string email);
        string StandardizeName(string name);
    }
}