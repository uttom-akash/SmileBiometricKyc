using smile_identity_core.Configs;
using smile_identity_core.Dtos;
using smile_identity_core.Utilities.CryptoSuit;
using smile_identity_core.Utilities.ZipSuit;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace smile_identity_core.Services
{
    public class WebApi
    {
        private readonly SmileIdentityConfig _smileConfig;
        private readonly IFilesZipper _filesZipper;
        private readonly ISignatureGenerator _signatureGenerator;

        public WebApi(SmileIdentityConfig smileConfig, IFilesZipper filesZipper, ISignatureGenerator signatureGenerator)
        {
            _smileConfig = smileConfig;
            _filesZipper = filesZipper;
            _signatureGenerator = signatureGenerator;
        }

        public async Task<TResponse> Post<TRequest, TResponse>(string uri, TRequest request)
        {
            var requestBody = JsonSerializer.Serialize(request);

            var client = new HttpClient();

            var response = await client
                .PostAsync($"{_smileConfig.BaseUrl}/upload",
                new StringContent(requestBody, Encoding.UTF8, "application/json"));

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Zip upload status code: {(int)response.StatusCode}");
            }

            var responseJson = JsonSerializer.Deserialize<TResponse>(await response.Content.ReadAsStringAsync());

            return responseJson;
        }

        public async Task<HttpResponseMessage> PutZip(string uri, byte[] zipFile)
        {
            using var client = new HttpClient();

            var zipContent = new ByteArrayContent(zipFile);

            zipContent.Headers.ContentType = new MediaTypeHeaderValue("application/zip");

            zipContent.Headers.ContentLength = zipFile.Length;

            var response = await client.PutAsync(uri, zipContent);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Zip upload status code: {(int)response.StatusCode}");
            }

            return response;
        }

        public async Task<BiometricKycInitResponse> InitBiometricKycJobAsync(PartnerParams partnerParams)
        {
            var signature = _signatureGenerator
                .GenerateSignature(_smileConfig.PartnerId, 
                    "2023-05-09T09:47:33.205Z",
                    _smileConfig.ApiKey);

            var biometricKycJobInitCommand = BiometricKycInitCommand
                .CreateBiometricKycJobInitCommand(partnerParams, _smileConfig, signature);

            var response = await Post<BiometricKycInitCommand, BiometricKycInitResponse>("/upload", biometricKycJobInitCommand);

            return response;
        }

        public async Task UploadRequirementsAsync(BiometricKycInitResponse biometricKycJobInitResponse, CustomerIdInformation customerIdInfo, List<ImageDetailDto> images)
        {
            var uploadCommand = BiometricKycInfoUploadCommand.Create(customerIdInfo, images);

            var zipFile = await _filesZipper.ZipFilesAsync(images, uploadCommand);

            await PutZip(biometricKycJobInitResponse.UploadUrl, zipFile);
        }

        public async Task SubmitJob(PartnerParams partnerParams,
            List<ImageDetailDto> imageDetailDtos,
            CustomerIdInformation customerIdInformation)
        {
            try
            {
                var response = await InitBiometricKycJobAsync(partnerParams);

                await UploadRequirementsAsync(response, customerIdInformation, imageDetailDtos);
            }
            catch (Exception err)
            {
                Console.WriteLine(err);
            }
        }

    }

}

