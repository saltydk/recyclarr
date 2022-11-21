using Recyclarr.Command;
using Spectre.Console.Cli;

namespace Recyclarr.Cli;

public static class Cli
{
    public static void Configure(IConfigurator cli)
    {
        cli.AddBranch("list", list =>
        {
            list.SetDescription("List information from the guide");
            list.AddCommand<ListCustomFormatsCommand>("custom-formats");
            list.AddCommand<ListReleaseProfilesCommand>("release-profiles");
            list.AddCommand<ListQualitiesCommand>("qualities");
        });

        cli.AddCommand<SyncCommand>("sync");
        cli.AddCommand<MigrateCommand>("migrate");

        cli.AddBranch("config", config =>
        {
            config.AddCommand<ConfigCreateCommand>("create");
        });
    }
}
