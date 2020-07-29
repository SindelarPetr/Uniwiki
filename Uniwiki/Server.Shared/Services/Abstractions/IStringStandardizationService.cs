﻿using System;

namespace Shared.Services.Abstractions
{
    public interface IStringStandardizationService
    {
        string CreateUrl(string text, Func<string, bool> isUniq);
        string OptimizeWhiteSpaces(string? text, string optimizeBy = " ");
        string StandardizeSearchText(string text);
        string RemoveAccents(string text);
        string StandardizeEmail(string email);
        string StandardizeName(string name);
        string StandardizeNameAndSurname(string nameAndSurname);
    }
}