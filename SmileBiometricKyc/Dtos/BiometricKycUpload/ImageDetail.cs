using System.IO;
using ExternalApi.BiometricKyc.SmileIdentityClient.Dtos.ClientDtos;
using Newtonsoft.Json;

namespace ExternalApi.BiometricKyc.SmileIdentityClient.Dtos.BiometricKycUpload;

public class ImageDetail
{

    [JsonProperty("image_type_id")]
    public int ImageTypeId { get; set; }

    [JsonProperty("image")]
    public string Image { get; set; }

    [JsonProperty("file_name")]
    public string FileName { get; set; }

    public static ImageDetail Create(ImageDto imageDetailDto)
    {
        return new ImageDetail
        {
            ImageTypeId = imageDetailDto.ImageTypeId,
            FileName = Path.GetFileName(imageDetailDto.ImageLink)
        };
    }
}