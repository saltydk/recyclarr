using System.Diagnostics;
using System.Threading.Tasks;
using Autofac;
using CliFx;
using Trash.Command.Helpers;

namespace Trash;

internal static class Program
{
    private static IContainer? _container;

    private static string ExecutableName => Process.GetCurrentProcess().ProcessName;

    public static async Task<int> Main()
    {
        _container = CompositionRoot.Setup();
        return await new CliApplicationBuilder()
            .AddCommandsFromThisAssembly()
            .SetExecutableName(ExecutableName)
            .SetVersion($"v{GitVersionInformation.MajorMinorPatch} (Build {GitVersionInformation.BuildMetaData})")
            .UseTypeActivator(type => CliTypeActivator.ResolveType(_container, type))
            .Build()
            .RunAsync();
    }
}
