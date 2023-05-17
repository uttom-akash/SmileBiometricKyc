namespace ExternalApi.BiometricKyc.SmileIdentityClient.Codes;

public enum JobType
{
    BiometricKyc = 1,
    
    SmartSelfieRegistration = 2,

    SmartSelfieAuthentication = 4,
    
    BasicKyc = 5,
    
    EnhancedKyc = 5,
    
    DocumentVerification = 6,
    
    BusinessVerification = 7
}