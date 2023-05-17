using System.Collections.Generic;

namespace ExternalApi.BiometricKyc.SmileIdentityClient.Dtos.ClientDtos
{
    public class BiometricKycSubmitCommand
    {
        public string CustomerGuid { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string IdType { get; set; }

        public string IdNumber { get; set; }

        public string Dob { get; set; }

        public string Country { get; set; }

        public List<ImageDto> Images { get; set; }
    }
}
