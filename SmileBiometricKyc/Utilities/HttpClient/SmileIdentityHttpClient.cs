using ExternalApi.BiometricKyc.SmileIdentityClient.Configs;
using ExternalApi.BiometricKyc.SmileIdentityClient.Constants;
using ExternalApi.BiometricKyc.SmileIdentityClient.Dtos.BiometricKycInit;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ExternalApi.BiometricKyc.SmileIdentityClient.Utilities.HttpClient
{
    public class SmileIdentityHttpClient : ISmileIdentityHttpClient
    {
        private readonly SmileIdentityKycConfig _smileConfig;
        private readonly System.Net.Http.HttpClient _httpClient;

        public SmileIdentityHttpClient(IOptions<SmileIdentityKycConfig> config,
            System.Net.Http.HttpClient httpClient)
        {
            _httpClient = httpClient;
            _smileConfig = config.Value;
        }

        public async Task<BiometricKycInitResponse> InitiateJobAsync(BiometricKycInitCommand command)
        {
            var requestBody = PrepareRequestBodyFromJson(command);

            var url = string.Concat(_smileConfig.BaseUrl, ConstantValues.Url.InitBiometricKycJob);

            var response = await _httpClient
                .PostAsync(url, requestBody);

            response.EnsureSuccessStatusCode();

            var responseJson = JsonConvert
                .DeserializeObject<BiometricKycInitResponse>(await response.Content.ReadAsStringAsync());

            return responseJson;
        }

        private static StringContent PrepareRequestBodyFromJson(BiometricKycInitCommand command)
        {
            return new StringContent(
                JsonConvert.SerializeObject(command),
                Encoding.UTF8, "application/json");
        }

        public async Task<HttpResponseMessage> UploadZipAsync(string uri, byte[] zipFile)
        {
            var zipContent = PrepareRequestBodyFromZip(zipFile);

            var response = await _httpClient.PutAsync(uri, zipContent);

            response.EnsureSuccessStatusCode();

            return response;
        }

        private static ByteArrayContent PrepareRequestBodyFromZip(byte[] zipFile)
        {
            var zipContent = new ByteArrayContent(zipFile);

            zipContent.Headers.ContentType = new MediaTypeHeaderValue("application/zip");

            zipContent.Headers.ContentLength = zipFile.Length;

            return zipContent;
        }

        public async Task<byte[]> DownloadFileContentAsync(string imageLink)
        {
            var response = await _httpClient.GetAsync(imageLink);

            response.EnsureSuccessStatusCode();

            var content = await response.Content
                .ReadAsByteArrayAsync();

            return content;
        }
    }

    public interface ISmileIdentityHttpClient
    {
        Task<HttpResponseMessage> UploadZipAsync(string uri, byte[] zipFile);

        Task<BiometricKycInitResponse> InitiateJobAsync(BiometricKycInitCommand command);

        Task<byte[]> DownloadFileContentAsync(string imageLink);
    }
}
