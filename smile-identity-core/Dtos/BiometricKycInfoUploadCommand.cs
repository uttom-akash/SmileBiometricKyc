using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;

namespace smile_identity_core.Dtos;

public class BiometricKycInfoUploadCommand
{
    [JsonPropertyName("package_information")]
    public PackageInformation PackageInformation { get; set; }


    [JsonPropertyName("id_info")]
    public CustomerIdInformation IdInfo { get; set; }


    [JsonPropertyName("images")]
    public List<ImageDetail> Images { get; set; }

    public static BiometricKycInfoUploadCommand Create(CustomerIdInformation customerIdInfo, List<ImageDetailDto> images)
    {
        var infoJson = new BiometricKycInfoUploadCommand
        {
            PackageInformation = PackageInformation.GetPackageInformation(),
            IdInfo = customerIdInfo,
            Images = images.Select(x=>ImageDetailDto.Create(x)).ToList(),
        };

        return infoJson;
    }

}