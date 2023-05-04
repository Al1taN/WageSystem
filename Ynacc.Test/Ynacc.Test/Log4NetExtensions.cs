using log4net;
using log4net.Config;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.IO;
using System.Reflection;

namespace Ynacc.Wage
{
    public static class Log4NetExtensions
    {
        public static IHostBuilder UseLog4Net(this IHostBuilder hostBuilder)
        {
            var log4netRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());
            XmlConfigurator.Configure(log4netRepository, new FileInfo("log4net.config"));
            hostBuilder.ConfigureLogging((hostcontext, logging) =>
                {
                    logging.ClearProviders();
                    logging.AddLog4Net();
                    logging.AddFilter("System", LogLevel.Warning);
                    logging.AddFilter("Microsoft", LogLevel.Warning);
                });
            return hostBuilder;
        }
    }
}
