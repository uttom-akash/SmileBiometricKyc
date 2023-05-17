using System;
using System.Text.Json.Serialization;

namespace smile_identity_core.Dtos;

public class PartnerParams
{
    [JsonPropertyName("job_type")]
    public int JobType { get; set; }

    [JsonPropertyName("job_id")]
    public string JobId { get; set; }

    [JsonPropertyName("user_id")]
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