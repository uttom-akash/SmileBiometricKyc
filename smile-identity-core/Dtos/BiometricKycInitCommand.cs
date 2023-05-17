using smile_identity_core.Configs;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace smile_identity_core.Dtos;

public class BiometricKycInitCommand
{
    [JsonPropertyName("source_sdk")]
    public string SourceSdk { get; set; } = "rest_api";

    [JsonPropertyName("source_sdk_version")]
    public string SourceSdkVersion { get; set; } = "1.0.0";

    [JsonPropertyName("file_name")]
    public string FileName { get; set; } = "biometrickyc.zip";

    [JsonPropertyName("signature")]
    public string Signature { get; set; }

    [JsonPropertyName("timestamp")]
    public string Timestamp { get; set; }

    [JsonPropertyName("smile_client_id")]
    public string SmileClientId { get; set; }

    [JsonPropertyName("partner_params")]
    public PartnerParams PartnerParams { get; set; }

    [JsonPropertyName("model_parameters")]
    public Dictionary<string, object> ModelParameters { get; set; } = new();

    [JsonPropertyName("callback_url")]
    public string CallbackUrl { get; set; }


    public static BiometricKycInitCommand CreateBiometricKycJobInitCommand(
        PartnerParams partnerParams,
        SmileIdentityConfig smileIdentityConfig,
        string signature)
    {
        var command = new BiometricKycInitCommand
        {
            CallbackUrl = smileIdentityConfig.CallbackUrl,
            PartnerParams = partnerParams,
            SmileClientId = smileIdentityConfig.PartnerId,
            Signature = signature,//"+a77c5ay4LEZP6jBlh6/VptYh8mGpX9QAUouvdTEBAc=",
            Timestamp = "2023-05-09T09:47:33.205Z"
        };

        return command;
    }
}