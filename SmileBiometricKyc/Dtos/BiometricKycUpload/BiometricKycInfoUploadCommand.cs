using System.Collections.Generic;
using System.Linq;
using ExternalApi.BiometricKyc.SmileIdentityClient.Dtos.ClientDtos;
using Newtonsoft.Json;

namespace ExternalApi.BiometricKyc.SmileIdentityClient.Dtos.BiometricKycUpload;

public class BiometricKycInfoUploadCommand
{
    [JsonProperty("package_information")]
    public PackageInformation PackageInformation { get; set; }


    [JsonProperty("id_info")]
    public CustomerIdInfo IdInfo { get; set; }


    [JsonProperty("images")]
    public List<ImageDetail> Images { get; set; }

    public static BiometricKycInfoUploadCommand Create(CustomerIdInfo customerIdInfo, List<ImageDto> images)
    {
        var infoJson = new BiometricKycInfoUploadCommand
        {
            PackageInformation = PackageInformation.GetPackageInformation(),
            IdInfo = customerIdInfo,
            Images = images.Select(ImageDetail.Create).ToList(),
        };

        return infoJson;
    }

}