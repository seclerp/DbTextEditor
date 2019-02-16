using System;
using System.Diagnostics;

namespace DbTextEditor.Shared
{
    public static class CommandLogger
    {
        public static void Log<TParent, TCommand>(string message)
        {
            var parentName = typeof(TParent).Name;
            var commandName = typeof(TCommand).Name;

            Debug.WriteLine($"[{parentName}]: {commandName} {message}");
        }
        public static void LogExecuted<TParent, TCommand>()
        {
            Log<TParent, TCommand>("executed");
        }

        public static void LogExecuted<TParent, TCommand>(object payload)
        {
            Log<TParent, TCommand>($"executed with payload {payload}");
        }
    }
}