using System.Text.Json.Serialization;

namespace smile_identity_core.Dtos;

public class BiometricKycInitResponse
{
    [JsonPropertyName("upload_url")]
    public string UploadUrl { get; set; }

    [JsonPropertyName("ref_id")]
    public string RefId { get; set; }

    [JsonPropertyName("smile_job_id")]
    public string SmileJobId { get; set; }

    [JsonPropertyName("camera_config")]
    public string CameraConfig { get; set; }

    [JsonPropertyName("code")]
    public string Code { get; set; }
}