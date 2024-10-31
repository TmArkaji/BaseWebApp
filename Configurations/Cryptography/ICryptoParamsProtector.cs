namespace BaseWebApplication.Configurations.Cryptography
{
    public interface ICryptoParamsProtector
    {

        string EncryptParamDictionary(Dictionary<string, string> parameters);

        Dictionary<string, string> DecryptToParamDictionary(string encryptedParameters);
    }
}
