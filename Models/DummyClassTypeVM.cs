namespace BaseWebApplication.Models
{
    public class DummyClassTypeVM : BaseEntityVM<int>
    {
        public override int ID { get; set; }
        public string ClassType { get; set; }
        public int ClassTypeOrder { get; set; }
    }
}
