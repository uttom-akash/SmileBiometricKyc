using System;
using System.Security.Cryptography;
using System.Text;

namespace ExternalApi.BiometricKyc.SmileIdentityClient.Utilities.CryptoSuit
{
    public class SignatureGenerator : ISignatureGenerator
    {
        public string GenerateSignature(string partnerId, string timestamp, string secretKey)
        {
            var apiKeyBytes = Encoding.UTF8.GetBytes(secretKey);

            var timestampBytes = Encoding.UTF8.GetBytes(timestamp);

            var partnerIdBytes = Encoding.UTF8.GetBytes(partnerId);

            var data = Encoding.UTF8.GetBytes("sid_request");

            using var hmac = new HMACSHA256(apiKeyBytes);

            hmac.Initialize();

            hmac.TransformBlock(timestampBytes, 0, timestampBytes.Length, timestampBytes, 0);

            hmac.TransformBlock(partnerIdBytes, 0, partnerIdBytes.Length, partnerIdBytes, 0);

            hmac.TransformFinalBlock(data, 0, data.Length);

            var macBytes = hmac.Hash;

            var mac = Convert.ToBase64String(macBytes);

            return mac;
        }
    }

    public interface ISignatureGenerator
    {
        string GenerateSignature(string partnerId, string timestamp, string secretKey);
    }
}
