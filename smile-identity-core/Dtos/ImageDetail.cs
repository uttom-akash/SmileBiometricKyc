using System.IO;
using System.Text.Json.Serialization;

namespace smile_identity_core.Dtos;

public class ImageDetail
{

    [JsonPropertyName("image_type_id")]
    public int ImageTypeId { get; set; }

    [JsonPropertyName("image")]
    public string Image { get; set; }

    [JsonPropertyName("file_name")]
    public string FileName { get; set; }
}

public class ImageDetailDto
{
    public int ImageTypeId { get; set; }
    
    public string ImageLink { get; set; }

    public static ImageDetail Create(ImageDetailDto imageDetailDto)
    {
        return new ImageDetail
        {
            ImageTypeId = imageDetailDto.ImageTypeId,
            FileName = Path.GetFileName(imageDetailDto.ImageLink)
        };
    }
}