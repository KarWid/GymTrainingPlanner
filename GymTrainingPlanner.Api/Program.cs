namespace GymTrainingPlanner.Api
{
    using System;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Hosting;
    using NLog;
    using NLog.Extensions.Logging;
    using GymTrainingPlanner.Common.Constants;

    public class Program
    {
        public static void Main(string[] args)
        {
            ConfigureNLog();
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });

        private static void ConfigureNLog()
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(System.IO.Directory.GetCurrentDirectory())
                .AddJsonFile(GetSettingsFileName(), optional: true, reloadOnChange: true).Build();

            LogManager.Configuration = new NLogLoggingConfiguration(config.GetSection("NLog"));
        }

        private static string GetSettingsFileName()
        {
            return Environment.GetEnvironmentVariable(Constant.EnvironmentVariablesConstant.ASPNETCORE_ENVIRONMENT) == Environments.Development
                ? "appsettings.Development.json" 
                : "appsettings.json";
        }
    }
}
