namespace BaseWebApplication.Data
{
    public class AppUserConfig : BaseEntity<int>
    {
        public override int ID { get; set; }
        public string appUserId { get; set; }
        public AppUser AppUser { get; set; }
    }
}
