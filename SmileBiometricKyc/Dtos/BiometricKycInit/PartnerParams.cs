using System;
using Newtonsoft.Json;

namespace ExternalApi.BiometricKyc.SmileIdentityClient.Dtos.BiometricKycInit;

public class PartnerParams
{
    [JsonProperty("job_type")]
    public int JobType { get; set; }

    [JsonProperty("job_id")]
    public string JobId { get; set; }

    [JsonProperty("user_id")]
    public string UserId { get; set; }

    public static PartnerParams Create(string userId, int jobType)
    {
        var data = new PartnerParams
        {
            JobId = Guid.NewGuid().ToString(),
            UserId = userId,
            JobType = jobType,
        };

        return data;
    }
}