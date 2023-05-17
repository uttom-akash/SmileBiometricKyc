using System.Collections.Generic;
using ExternalApi.BiometricKyc.SmileIdentityClient.Configs;
using Newtonsoft.Json;

namespace ExternalApi.BiometricKyc.SmileIdentityClient.Dtos.BiometricKycInit;

public class BiometricKycInitCommand
{
    [JsonProperty("source_sdk")]
    public string SourceSdk { get; set; } = "rest_api";

    [JsonProperty("source_sdk_version")]
    public string SourceSdkVersion { get; set; } = "1.0.0";

    [JsonProperty("file_name")]
    public string FileName { get; set; } = "biometrickyc.zip";

    [JsonProperty("signature")]
    public string Signature { get; set; }

    [JsonProperty("timestamp")]
    public string Timestamp { get; set; }

    [JsonProperty("smile_client_id")]
    public string SmileClientId { get; set; }

    [JsonProperty("partner_params")]
    public PartnerParams PartnerParams { get; set; }

    [JsonProperty("model_parameters")]
    public Dictionary<string, object> ModelParameters { get; set; } = new();

    [JsonProperty("callback_url")]
    public string CallbackUrl { get; set; }


    public static BiometricKycInitCommand CreateBiometricKycJobInitCommand(
        PartnerParams partnerParams,
        SmileIdentityKycConfig smileIdentityConfig,
        string signature,
        string timestamp)
    {
        var command = new BiometricKycInitCommand
        {
            CallbackUrl = smileIdentityConfig.CallbackUrl,
            PartnerParams = partnerParams,
            SmileClientId = smileIdentityConfig.PartnerId,
            Signature = signature,
            Timestamp = timestamp
        };

        return command;
    }
}