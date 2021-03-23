namespace MySerilogLibrary
{
    using Serilog.Events;

    /// <summary>
    /// appsettings config section
    ///
    ///   "MySerilogLibrary": {
    ///     "ApplicationName": "my-serilog-library-api",
    ///     "Environment": "Development",
    ///     "MinimumLevel": "Information",
    ///     "LokiUrl": "http://localhost:3100",
    ///     "SeqUrl": "http://localhost:5341"
    ///   }
    ///
    /// </summary>
    internal class AppSettingsConfiguration
    {
        internal const string Name = "MySerilogLibrary";

        public string ApplicationName { get; set; }
        public string Environment { get; set; }
        public LogEventLevel MinimumLevel { get; set; } = LogEventLevel.Information;
        public string LokiUrl { get; set; }
        public string SeqUrl { get; set; }
    }
}