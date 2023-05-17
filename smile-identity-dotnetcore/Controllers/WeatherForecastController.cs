using ExternalApi.BiometricKyc.SmileIdentityClient.Dtos.ClientDtos;
using ExternalApi.BiometricKyc.SmileIdentityClient.Services;
using Microsoft.AspNetCore.Mvc;
using smile_identity_core;
using smile_identity_core.Configs;
using smile_identity_core.Dtos;
using smile_identity_core.Services;
using smile_identity_core.Utilities.CryptoSuit;
using smile_identity_core.Utilities.ZipSuit;

namespace smile_identity_dotnetcore.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly ISmileBiometricKycService _biometric;


        public WeatherForecastController(ILogger<WeatherForecastController> logger, ISmileBiometricKycService biometric)
        {
            _logger = logger;
            _biometric = biometric;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public async Task Get()
        {
            var partnerParams = PartnerParams.Create("john_doe", 1);

            var cusIdInfo = new CustomerIdInformation
            {
                FirstName = "john",
                LastName = "<surname>",
                Country = "NG",
                IdType = "VOTER_ID",
                IdNumber = "0000000000000000000",
                Dob = "<date of birth>", // yyyy-mm-dd
                Entered = true,
            };

            var images = new List<ImageDetailDto>()
            {
                new ImageDetailDto(){
                    ImageTypeId = 0,
                    ImageLink = "https://ibb.co/nD9GYLM"//"https://ibb.co/mCRGSMW"
                },
                new ImageDetailDto(){
                    ImageTypeId = 1,
                    ImageLink = "https://ibb.co/GTZPsLp"
                }
            };

            await _biometric.SubmitBiometricKycAsync(new BiometricKycSubmitCommand
            {

            });

            //await new WebApi(new SmileIdentityConfig
            //{
            //    ApiKey = "551d3733-cab1-45f6-b0ea-2927ef79b1df",
            //    PartnerId = "2448",
            //    CallbackUrl = "https://smile.free.beeceptor.com/smile",
            //    BaseUrl = "https://testapi.smileidentity.com/v1"
            //}, new FilesZipper(), new SignatureGenerator())
            //    .SubmitJob(partnerParams, images, cusIdInfo);

        }
    }
}