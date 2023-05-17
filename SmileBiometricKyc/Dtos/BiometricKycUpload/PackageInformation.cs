using Newtonsoft.Json;

namespace ExternalApi.BiometricKyc.SmileIdentityClient.Dtos.BiometricKycUpload;

public class PackageInformation
{

    [JsonProperty("apiVersion")]
    public ApiVersion ApiVersion { get; set; }

    public static PackageInformation GetPackageInformation() =>
        new()
        {
            ApiVersion = ApiVersion.GetApiVersion()
        };
}