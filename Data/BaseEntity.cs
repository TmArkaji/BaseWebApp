namespace BaseWebApplication.Data
{
    public abstract class BaseEntity<TKey>
    {
        public abstract TKey ID { get; set; }
        public DateTime CreateDate { get; set; }
        public string CreateUserId { get; set; }
        public DateTime UpdateDate { get; set; }
        public string UpdateUserId { get; set; }
        public bool Eliminado { get; set; }
    }
}
