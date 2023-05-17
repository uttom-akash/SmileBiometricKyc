using ExternalApi.BiometricKyc.SmileIdentityClient.Configs;
using ExternalApi.BiometricKyc.SmileIdentityClient.Services;
using ExternalApi.BiometricKyc.SmileIdentityClient.Utilities.CryptoSuit;
using ExternalApi.BiometricKyc.SmileIdentityClient.Utilities.HttpClient;
using ExternalApi.BiometricKyc.SmileIdentityClient.Utilities.ZipSuit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ExternalApi.BiometricKyc.SmileIdentityClient.Extensions
{
    public static class SmileBiometricKycExtensions
    {
        public static IServiceCollection RegisterSmileIdentityKyc(this IServiceCollection service, IConfiguration configuration)
        {
            service.Configure<SmileIdentityKycConfig>(configuration.GetSection(nameof(SmileIdentityKycConfig)));

            service.AddScoped<ISmileBiometricKycService, SmileBiometricKycService>();
            service.AddScoped<ISignatureGenerator, SignatureGenerator>();
            service.AddScoped<ISmileIdentityHttpClient, SmileIdentityHttpClient>();
            service.AddScoped<IFilesZipper, FilesZipper>();

            return service;
        }
    }
}
