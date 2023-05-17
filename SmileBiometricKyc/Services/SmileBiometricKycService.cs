using ExternalApi.BiometricKyc.SmileIdentityClient.Codes;
using ExternalApi.BiometricKyc.SmileIdentityClient.Configs;
using ExternalApi.BiometricKyc.SmileIdentityClient.Constants;
using ExternalApi.BiometricKyc.SmileIdentityClient.Dtos.BiometricKycInit;
using ExternalApi.BiometricKyc.SmileIdentityClient.Dtos.BiometricKycUpload;
using ExternalApi.BiometricKyc.SmileIdentityClient.Dtos.ClientDtos;
using ExternalApi.BiometricKyc.SmileIdentityClient.Utilities.CryptoSuit;
using ExternalApi.BiometricKyc.SmileIdentityClient.Utilities.HttpClient;
using ExternalApi.BiometricKyc.SmileIdentityClient.Utilities.ZipSuit;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ExternalApi.BiometricKyc.SmileIdentityClient.Services
{
    public class SmileBiometricKycService : ISmileBiometricKycService
    {
        private readonly SmileIdentityKycConfig _smileConfig;
        private readonly IFilesZipper _filesZipper;
        private readonly ISignatureGenerator _signatureGenerator;
        private readonly ISmileIdentityHttpClient _smileIdentityHttpClient;

        public SmileBiometricKycService(SmileIdentityKycConfig smileConfig,
            IFilesZipper filesZipper,
            ISignatureGenerator signatureGenerator,
            ISmileIdentityHttpClient smileIdentityHttpClient)
        {
            _smileConfig = smileConfig;
            _filesZipper = filesZipper;
            _signatureGenerator = signatureGenerator;
            _smileIdentityHttpClient = smileIdentityHttpClient;
        }

        public async Task<BiometricKycSubmitResponse> SubmitBiometricKycAsync(BiometricKycSubmitCommand biometricKycVerifyCommand)
        {
            var partnerParams = PartnerParams.Create(
                    biometricKycVerifyCommand.CustomerGuid,
                    (short)JobType.BiometricKyc);

            var customerIdInformation = CustomerIdInfo.Create(biometricKycVerifyCommand);

            var biometricKycInitResponse = await InitBiometricKycJobAsync(partnerParams);

            await UploadBiometricKycRequirementsAsync(biometricKycInitResponse,
                customerIdInformation,
                biometricKycVerifyCommand.Images);

            return new BiometricKycSubmitResponse
            {
                SessionTrackerId = partnerParams.JobId,
                ExternalReferenceId = biometricKycInitResponse.SmileJobId
            };
        }

        private async Task<BiometricKycInitResponse> InitBiometricKycJobAsync(PartnerParams partnerParams)
        {
            var timestamp = DateTime.UtcNow.ToString(ConstantValues.DateFormat);

            var signature = _signatureGenerator
                .GenerateSignature(
                    _smileConfig.PartnerId,
                    timestamp,
                    _smileConfig.ApiKey);

            var biometricKycJobInitCommand = BiometricKycInitCommand
                .CreateBiometricKycJobInitCommand(
                    partnerParams,
                    _smileConfig,
                    signature,
                    timestamp);

            var response = await _smileIdentityHttpClient.InitiateJobAsync(biometricKycJobInitCommand);


            return response;
        }

        private async Task UploadBiometricKycRequirementsAsync(BiometricKycInitResponse biometricKycJobInitResponse, CustomerIdInfo customerIdInfo, List<ImageDto> images)
        {
            var uploadCommand = BiometricKycInfoUploadCommand
                .Create(customerIdInfo, images);

            var zipFile = await _filesZipper
                .ZipFilesAsync(images, uploadCommand);

            var response = await _smileIdentityHttpClient.UploadZipAsync(biometricKycJobInitResponse.UploadUrl, zipFile);
        }

    }

    public interface ISmileBiometricKycService
    {
        Task<BiometricKycSubmitResponse> SubmitBiometricKycAsync(BiometricKycSubmitCommand biometricKycVerifyCommand);
    }
}

