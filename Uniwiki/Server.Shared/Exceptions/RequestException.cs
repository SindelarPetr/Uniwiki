using System;
using System.Runtime.CompilerServices;
using Shared.Dtos;

namespace Shared.Exceptions
{
    public class RequestException : Exception
    {
        public string HumanMessage { get; }
        public string? DebugMessage { get; }
        public FixResponseDto[] Fixes { get; }

        public RequestException(string humanMessage, FixResponseDto[]? fixes = null, string? debugMessage = null, [CallerMemberName] string callerMemberName = "", [CallerLineNumber] int callerLineNumber = 0, [CallerFilePath] string callerFilePath = "")
        {
            HumanMessage = humanMessage;
            Fixes = fixes ?? new FixResponseDto[0];
            DebugMessage = debugMessage;
        }
    }
}
