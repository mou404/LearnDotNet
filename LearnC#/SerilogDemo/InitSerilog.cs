﻿
using Serilog;
using Serilog.Core;
using Serilog.Events;

namespace SerilogDemo
{
    public class InitSerilog
    {

        public InitSerilog()
        {
            var configuration = new ConfigurationBuilder()
       .SetBasePath(Directory.GetCurrentDirectory())
       .AddJsonFile("appsettings.json")
       .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", true)
       .Build();

            var logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .CreateLogger();

            logger.Information("Hello, world!");
            Log.Logger = new LoggerConfiguration()
                            //为日志信息添加额外信息:线程号
                            .Enrich.With(new ThreadIdEnricher())
                            .WriteTo.Console(restrictedToMinimumLevel: LogEventLevel.Information)
                            //最小日志等级
                            .MinimumLevel.Debug()
                            .WriteTo.File("log-.txt", outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}")
                            .CreateLogger();
        }

        private class ThreadIdEnricher : ILogEventEnricher
        {
            public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
            {
                logEvent.AddPropertyIfAbsent(propertyFactory.CreateProperty(
                        "ThreadId", Thread.CurrentThread.ManagedThreadId));
            }
        }
    }
}