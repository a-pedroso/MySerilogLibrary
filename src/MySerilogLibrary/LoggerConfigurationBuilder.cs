namespace MySerilogLibrary
{
    using Serilog;
    using Serilog.Events;
    using Serilog.Sinks.Loki;
    using System;

    internal static class LoggerConfigurationBuilder
    {
        internal static LoggerConfiguration Build(AppSettingsConfiguration config)
        {
            return new LoggerConfiguration().ConfigureFilters()
                                            .ConfigureEnrichers(config.ApplicationName, config.Environment)
                                            .ConfigureMinimumLevel(config.MinimumLevel)
                                            .ConfigureWriteTo(config.LokiUrl, config.SeqUrl);
        }

        private static LoggerConfiguration ConfigureFilters(this LoggerConfiguration loggerConfiguration)
        {
            return loggerConfiguration.Filter.ByExcluding("RequestPath like '/metrics%'")
                                      .Filter.ByExcluding("RequestPath like '/health%'")
                                      .Filter.ByExcluding("RequestPath like '/swagger%'");
        }

        private static LoggerConfiguration ConfigureEnrichers(this LoggerConfiguration loggerConfiguration, string applicationName, string environment)
        {
            return loggerConfiguration.Enrich.WithProperty("ApplicationName", applicationName)
                                      .Enrich.WithProperty("Environment", environment)
                                      .Enrich.FromLogContext()
                                      .Enrich.WithCorrelationId()
                                      .Enrich.WithCorrelationIdHeader()
                                      .Enrich.WithEnvironmentUserName()
                                      .Enrich.WithMachineName()
                                      .Enrich.WithProcessId()
                                      .Enrich.WithProcessName()
                                      .Enrich.WithThreadId()
                                      .Enrich.WithThreadName();
        }

        private static LoggerConfiguration ConfigureMinimumLevel(this LoggerConfiguration loggerConfiguration, LogEventLevel minimumLevel)
        {
            loggerConfiguration = minimumLevel switch
            {
                LogEventLevel.Verbose => loggerConfiguration.MinimumLevel.Verbose(),
                LogEventLevel.Debug => loggerConfiguration.MinimumLevel.Debug(),
                LogEventLevel.Information => loggerConfiguration.MinimumLevel.Information(),
                LogEventLevel.Warning => loggerConfiguration.MinimumLevel.Warning(),
                LogEventLevel.Error => loggerConfiguration.MinimumLevel.Error(),
                LogEventLevel.Fatal => loggerConfiguration.MinimumLevel.Fatal(),
                _ => throw new NotImplementedException()
            };

            return loggerConfiguration.MinimumLevel.Override("Microsoft.AspNetCore", minimumLevel)
                                      .MinimumLevel.Override("System", minimumLevel)
                                      .MinimumLevel.Override("Microsoft", minimumLevel)
                                      .MinimumLevel.Override("Microsoft.Hosting.Lifetime", minimumLevel);
        }

        private static LoggerConfiguration ConfigureWriteTo(this LoggerConfiguration loggerConfiguration, string lokiUrl, string seqUrl)
        {
            return loggerConfiguration.WriteTo.Debug()
                                      .WriteTo.Async(a => a.Console(outputTemplate: Defaults.ConsoleOutputTemplate))
                                      .WriteTo.LokiHttp(lokiUrl)
                                      .WriteTo.Seq(seqUrl);
        }
    }
}