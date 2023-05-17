using ExternalApi.BiometricKyc.SmileIdentityClient.Dtos.ClientDtos;
using Newtonsoft.Json;

namespace ExternalApi.BiometricKyc.SmileIdentityClient.Dtos.BiometricKycUpload;

public class CustomerIdInfo
{
    [JsonProperty("first_name")]
    public string FirstName { get; set; }

    [JsonProperty("middle_name")]
    public string MiddleName { get; set; }

    [JsonProperty("last_name")]
    public string LastName { get; set; }

    [JsonProperty("id_type")]
    public string IdType { get; set; }

    [JsonProperty("id_number")]
    public string IdNumber { get; set; }

    [JsonProperty("dob")]
    public string Dob { get; set; }

    [JsonProperty("country")]
    public string Country { get; set; }

    [JsonProperty("entered")]
    public bool Entered { get; set; }

    [JsonProperty("phone_number")]
    public string PhoneNumber;

    [JsonProperty("business_type")]
    public string BusinessType { get; set; }

    [JsonProperty("postal_address")]
    public string PostalAddress { get; set; }

    [JsonProperty("postal_code")]
    public string PostalCode { get; set; }

    public static CustomerIdInfo Create(BiometricKycSubmitCommand customerInfo)
    {
        return new CustomerIdInfo
        {
            FirstName = customerInfo.FirstName,
            LastName = customerInfo.LastName,
            IdType = customerInfo.IdType,
            IdNumber = customerInfo.IdNumber,
            Country = customerInfo.Country,
            Dob = customerInfo.Dob,
        };
    }
}