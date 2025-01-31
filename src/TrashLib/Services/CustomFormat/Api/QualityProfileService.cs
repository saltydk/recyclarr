using Flurl.Http;
using Newtonsoft.Json.Linq;
using TrashLib.Config.Services;

namespace TrashLib.Services.CustomFormat.Api;

internal class QualityProfileService : IQualityProfileService
{
    private readonly IServiceRequestBuilder _service;

    public QualityProfileService(IServiceRequestBuilder service)
    {
        _service = service;
    }

    public async Task<List<JObject>> GetQualityProfiles()
    {
        return await _service.Request("qualityprofile")
            .GetJsonAsync<List<JObject>>();
    }

    public async Task<JObject> UpdateQualityProfile(JObject profileJson, int id)
    {
        return await _service.Request("qualityprofile", id)
            .PutJsonAsync(profileJson)
            .ReceiveJson<JObject>();
    }
}
