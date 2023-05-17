using Newtonsoft.Json;

namespace ExternalApi.BiometricKyc.SmileIdentityClient.Dtos.BiometricKycUpload;

public class ApiVersion
{
    [JsonProperty("buildNumber")]
    public int BuildNumber { get; set; }


    [JsonProperty("majorVersion")]
    public int MajorVersion { get; set; }


    [JsonProperty("minorVersion")]
    public int MinorVersion { get; set; }


    public static ApiVersion GetApiVersion() =>
        new()
        {
            BuildNumber = 0,
            MajorVersion = 2,
            MinorVersion = 0
        };
}