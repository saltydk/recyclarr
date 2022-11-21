using System.Data.HashFunction.FNV;
using System.IO.Abstractions;
using System.Text;
using Recyclarr.Command;
using TrashLib.Cache;
using TrashLib.Config.Services;
using TrashLib.Startup;

namespace Recyclarr.Config;

public class CacheStoragePath : ICacheStoragePath
{
    private readonly IAppPaths _paths;
    private readonly IServiceCommand _serviceCommand;
    private readonly IServiceConfiguration _config;
    private readonly IFNV1a _hash;

    public CacheStoragePath(
        IAppPaths paths,
        IServiceCommand serviceCommand,
        IServiceConfiguration config)
    {
        _paths = paths;
        _serviceCommand = serviceCommand;
        _config = config;
        _hash = FNV1aFactory.Instance.Create(FNVConfig.GetPredefinedConfig(32));
    }

    private string BuildServiceGuid()
    {
        return _hash.ComputeHash(Encoding.ASCII.GetBytes(_config.BaseUrl)).AsHexString();
    }

    public IFileInfo CalculatePath(string cacheObjectName)
    {
        return _paths.CacheDirectory
            .SubDirectory(_serviceCommand.Name.ToLower())
            .SubDirectory(_config.Name.Any() ? _config.Name : BuildServiceGuid())
            .File(cacheObjectName + ".json");
    }
}
