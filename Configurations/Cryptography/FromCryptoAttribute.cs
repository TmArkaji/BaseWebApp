using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace BaseWebApplication.Configurations.Cryptography
{
    [AttributeUsage(AttributeTargets.Parameter)]
    public class FromCryptoAttribute : Attribute, IBindingSourceMetadata
    {
        public BindingSource BindingSource => CryptoBindingSource.Crypto;
    }
}
