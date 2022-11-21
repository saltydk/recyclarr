using Autofac;
using Recyclarr.Cli.Helpers;
using Serilog.Core;
using Spectre.Console.Cli;

namespace Recyclarr;

internal static class Program
{
    // private static string ExecutableName => Process.GetCurrentProcess().ProcessName;

    public static int Main(string[] args)
    {
        var builder = new ContainerBuilder();
        CompositionRoot.Setup(builder);

        var logLevelSwitch = new LoggingLevelSwitch();
        builder.RegisterInstance(logLevelSwitch);

        var app = new CommandApp(new AutofacTypeRegistrar(builder));
        app.Configure(config =>
        {
            // todo: Possibly reintroduce these if the defaults do not suffice.
            // config.SetApplicationName("recyclarr");
            // config.SetApplicationVersion("v1.2.3");
            config.SetInterceptor(new LogInterceptor(logLevelSwitch));
            Cli.Configure(config);
        });

        var result = app.Run(args);

        return result;
    }

    // private static IEnumerable<Type> GetAllCommandTypes()
    // {
    //     return typeof(Program).Assembly.GetTypes()
    //         .Where(x => x.IsAssignableTo<CommandSettings>() && !x.IsAbstract);
    // }
    //
    // private static string BuildVersion()
    // {
    //     var builder = new StringBuilder($"v{GitVersionInformation.MajorMinorPatch}");
    //     var metadata = GitVersionInformation.FullBuildMetaData;
    //     if (!string.IsNullOrEmpty(metadata))
    //     {
    //         builder.Append($" ({metadata})");
    //     }
    //
    //     return builder.ToString();
    // }
}
