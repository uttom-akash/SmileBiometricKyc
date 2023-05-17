using Newtonsoft.Json;

namespace ExternalApi.BiometricKyc.SmileIdentityClient.Dtos.BiometricKycInit;

public class BiometricKycInitResponse
{
    [JsonProperty("upload_url")]
    public string UploadUrl { get; set; }

    [JsonProperty("ref_id")]
    public string RefId { get; set; }

    [JsonProperty("smile_job_id")]
    public string SmileJobId { get; set; }

    [JsonProperty("camera_config")]
    public string CameraConfig { get; set; }

    [JsonProperty("code")]
    public string Code { get; set; }
}