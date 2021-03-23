namespace MySerilogLibrary
{
    using Microsoft.Extensions.Configuration;
    using Serilog;
    using Serilog.Core;
    using System;
    using System.IO;

    // serilog setup
    // https://nblumhardt.com/2019/10/serilog-in-aspnetcore-3/
    // https://github.com/serilog/serilog-aspnetcore/blob/71165692d5f66c811c3b251047b12c259ac2fe23/samples/EarlyInitializationSample/Program.cs#L12

    public static class Setup
    {
        public static IConfiguration Configuration { get; } = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", optional: true)
            .AddEnvironmentVariables()
            .Build();

        /// <summary>
        /// Call this method on Program to get Serilog Logger
        /// </summary>
        /// <returns>Serilog.Core.Logger Instance</returns>
        public static Logger GetLogger()
        {
            AppSettingsConfiguration appSettingsConfiguration = new();

            Configuration.GetSection(AppSettingsConfiguration.Name)
                         .Bind(appSettingsConfiguration);

            LoggerConfiguration configuration = LoggerConfigurationBuilder.Build(appSettingsConfiguration);
            return configuration.CreateLogger();
        }
    }
}