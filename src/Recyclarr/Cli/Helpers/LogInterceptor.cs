using Serilog.Core;
using Serilog.Events;
using Spectre.Console.Cli;

namespace Recyclarr.Cli.Helpers;

public class LogInterceptor : ICommandInterceptor
{
    private readonly LoggingLevelSwitch _loggingLevelSwitch;

    public LogInterceptor(LoggingLevelSwitch loggingLevelSwitch)
    {
        _loggingLevelSwitch = loggingLevelSwitch;
    }

    public void Intercept(CommandContext context, CommandSettings settings)
    {
        if (settings is BaseCommandSettings baseCmd)
        {
            _loggingLevelSwitch.MinimumLevel = baseCmd.Debug switch
            {
                true => LogEventLevel.Debug,
                _ => LogEventLevel.Information
            };
        }
    }
}
