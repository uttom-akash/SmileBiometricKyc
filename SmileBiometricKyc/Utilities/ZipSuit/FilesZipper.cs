using ExternalApi.BiometricKyc.SmileIdentityClient.Constants;
using ExternalApi.BiometricKyc.SmileIdentityClient.Dtos.BiometricKycUpload;
using ExternalApi.BiometricKyc.SmileIdentityClient.Dtos.ClientDtos;
using ExternalApi.BiometricKyc.SmileIdentityClient.Utilities.HttpClient;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Text;
using System.Threading.Tasks;

namespace ExternalApi.BiometricKyc.SmileIdentityClient.Utilities.ZipSuit
{
    public class FilesZipper : IFilesZipper
    {
        private readonly ISmileIdentityHttpClient _smileIdentityHttpClient;

        public FilesZipper(ISmileIdentityHttpClient smileIdentityHttpClient)
        {
            _smileIdentityHttpClient = smileIdentityHttpClient;
        }

        public async Task<byte[]> ZipFilesAsync(List<ImageDto> images, BiometricKycInfoUploadCommand infoJson)
        {
            using var memoryStream = new MemoryStream();

            using var zipArchive = new ZipArchive(memoryStream, ZipArchiveMode.Create, true);


            AddFileToZip(zipArchive, ConstantValues.InfoFilename, Encoding.ASCII.GetBytes(JsonConvert.SerializeObject(infoJson)));

            foreach (var image in images)
            {
                var imageContent = await _smileIdentityHttpClient.DownloadFileContentAsync(image.ImageLink);

                AddFileToZip(zipArchive, Path.GetFileName(image.ImageLink), imageContent);
            }

            zipArchive.Dispose();

            return memoryStream.ToArray();
        }

        private void AddFileToZip(ZipArchive zipArchive, string fileName, byte[] fileContents)
        {
            var entry = zipArchive
                .CreateEntry(fileName);

            using var entryStream = entry
                .Open();

            entryStream
                .Write(fileContents, 0, fileContents.Length);
        }


    }

    public interface IFilesZipper
    {
        Task<byte[]> ZipFilesAsync(List<ImageDto> images, BiometricKycInfoUploadCommand infoJson);
    }
}
