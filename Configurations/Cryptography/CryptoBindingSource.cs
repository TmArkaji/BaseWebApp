using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace BaseWebApplication.Configurations.Cryptography
{
    public class CryptoBindingSource
    {
        public static readonly BindingSource Crypto = new BindingSource(
            "Crypto",
            "BindingSource_Crypto",
            isGreedy: false,
            isFromRequest: true);
    }
}
