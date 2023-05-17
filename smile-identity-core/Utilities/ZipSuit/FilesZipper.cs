using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using smile_identity_core.Dtos;

namespace smile_identity_core.Utilities.ZipSuit
{
    public  class FilesZipper : IFilesZipper
    {
        
        public async Task<byte[]> ZipFilesAsync(List<ImageDetailDto> images, BiometricKycInfoUploadCommand infoJson)
        {
            using var memoryStream = new MemoryStream();

            using var zipArchive = new ZipArchive(memoryStream, ZipArchiveMode.Create, true);

            AddFileToZip(zipArchive, "info.json", Encoding.ASCII.GetBytes(JsonSerializer.Serialize(infoJson)));

            foreach (var image in images)
            {
                var imageContent = await DownloadFileContentAsync(image.ImageLink);

                AddFileToZip(zipArchive, Path.GetFileName(image.ImageLink), imageContent);
            }

            zipArchive.Dispose();

            var zipFileBytes = memoryStream.ToArray();

            return zipFileBytes;
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

        private async Task<byte[]> DownloadFileContentAsync(string blobLink)
        {
            using var client = new HttpClient();

            var file = await client
                .GetAsync(blobLink);

            file
                .EnsureSuccessStatusCode();

            var content = await file
                .Content
                .ReadAsByteArrayAsync();

            return content;
        }
    }

    public interface IFilesZipper
    {
        Task<byte[]> ZipFilesAsync(List<ImageDetailDto> images, BiometricKycInfoUploadCommand infoJson);
    }
}
