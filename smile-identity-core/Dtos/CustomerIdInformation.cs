using System.Text.Json.Serialization;

namespace smile_identity_core.Dtos;

public class CustomerIdInformation
{

    [JsonPropertyName("first_name")]
    public string FirstName { get; set; }

    [JsonPropertyName("middle_name")]
    public string MiddleName { get; set; }

    [JsonPropertyName("last_name")]
    public string LastName { get; set; }

    [JsonPropertyName("id_type")]
    public string IdType { get; set; }

    [JsonPropertyName("id_number")]
    public string IdNumber { get; set; }

    [JsonPropertyName("dob")]
    public string Dob { get; set; }

    [JsonPropertyName("country")]
    public string Country { get; set; }

    [JsonPropertyName("entered")]
    public bool Entered { get; set; }

    [JsonPropertyName("phone_number")]
    public string PhoneNumber;

    [JsonPropertyName("business_type")]
    public string BusinessType { get; set; }

    [JsonPropertyName("postal_address")]
    public string PostalAddress { get; set; }

    [JsonPropertyName("postal_code")]
    public string PostalCode { get; set; }
}