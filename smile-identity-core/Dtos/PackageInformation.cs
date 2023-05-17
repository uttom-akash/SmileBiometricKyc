using System.Text.Json.Serialization;

namespace smile_identity_core.Dtos;

public class PackageInformation
{

    [JsonPropertyName("apiVersion")]
    public ApiVersion ApiVersion { get; set; }

    public static PackageInformation GetPackageInformation() =>
        new()
        {
            ApiVersion = ApiVersion.GetApiVersion()
        };
}

public class ApiVersion
{
    [JsonPropertyName("buildNumber")]
    public int BuildNumber { get; set; }


    [JsonPropertyName("majorVersion")]
    public int MajorVersion { get; set; }


    [JsonPropertyName("minorVersion")]
    public int MinorVersion { get; set; }


    public static ApiVersion GetApiVersion() =>
        new()
        {
            BuildNumber = 0,
            MajorVersion = 2,
            MinorVersion = 0
        };
}