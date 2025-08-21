using EasyConsoleCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting.Internal;
using Microsoft.Extensions.Logging;
using StarWars.Service;

namespace StarWars.CSV
{
    internal class Program
    {
        static bool exit = false;
        static Menu menu = null!;

        public static IConfiguration Configuration { get; private set; } = null!;
        public static ServiceProvider ServiceProvider { get; private set; } = null!;

        static IConfigurationBuilder Configure(IConfigurationBuilder config, string environmentName)
        {
            return config
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{environmentName}.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables();
        }

        static IConfiguration CreateConfiguration()
        {
            var env = new HostingEnvironment
            {
                EnvironmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production",
                ApplicationName = AppDomain.CurrentDomain.FriendlyName,
                ContentRootPath = AppDomain.CurrentDomain.BaseDirectory,
                ContentRootFileProvider = new PhysicalFileProvider(AppDomain.CurrentDomain.BaseDirectory)
            };

            var config = new ConfigurationBuilder();
            var configured = Configure(config, env.EnvironmentName);
            return configured.Build();
        }

        static void Run()
        {
            menu = new Menu()
                .Add("Movies", new Action(async () => await StarWars.Movies()))
                .Add("Movie Ratings", new Action(async () => await StarWars.MovieRatings()))
                .Add("Lookup", new Action(async () => await StarWars.Lookup()))
                .Add("Exit", () => { exit = true; });

            while (!exit)
            {
                menu.Display();
            }
        }

        static void Main()
        {
            Configuration = CreateConfiguration();

            // Configure Services
            var services = new ServiceCollection()
                .AddLogging(configure => configure.AddConsole())
                .Configure<LoggerFilterOptions>(options => options.MinLevel = LogLevel.Debug)
                .AddSingleton(Configuration)
                .AddSingleton<IStarWarsService, StarWarsService>();

            ServiceProvider = services.BuildServiceProvider();
            Run();
        }
    }
}
