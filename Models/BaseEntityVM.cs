namespace BaseWebApplication.Models
{
    public abstract class BaseEntityVM<TKey>
    {
        public abstract TKey ID { get; set; }

        public string? EncryptedID { get; set; }
    }
}
