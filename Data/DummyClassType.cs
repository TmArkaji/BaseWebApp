namespace BaseWebApplication.Data
{
    public class DummyClassType : BaseEntity<int>
    {
        public override int ID { get; set; }
        public string ClassType { get; set; }
        public int ClassTypeOrder { get; set; }
    }
}
