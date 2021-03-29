namespace MySerilogLibrary
{
    internal static class Defaults
    {
        internal const string ConsoleOutputTemplate = "{Timestamp:yyyy-MM-dd HH:mm:ss.fff} [{Level}] {Message:lj} MachineName:{MachineName} ProcessId:{ProcessId} ThreadId:{ThreadId} EnvironmentUserName:{EnvironmentUserName} RequestId:{RequestId} SourceContext:{SourceContext} {Exception} {NewLine}";
    }
}