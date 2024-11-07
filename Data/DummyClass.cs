using BaseWebApplication.Configurations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BaseWebApplication.Data
{
    public class DummyClass : BaseEntity<string>
    {
        public override string ID { get; set; }
        public string Dummy { get; set; }

        public DateTime DateField { get; set; }
        public DateTime DateTimeField { get; set; }
        public DateTime? NulableDateField { get; set; }

        [Column(TypeName = Constants.COLUMN_TYPE_DECIMAL)]
        public decimal DecimalField { get; set; }
        public int IntField { get; set; }

        public string? TextAreaField { get; set; }

        public bool BoolField { get; set; }

        #region Relaciones

        public int DummyClassTypeID { get; set; }
        public virtual DummyClassType DummyClassType { get; set; }

        #endregion

        #region Calculate Fields

        public string Conditional
        {
            get
            {
                return (DecimalField > IntField) ? nameof(DecimalField) : nameof(IntField);
            }
        }

        public string DisplayText
        {
            get
            {
                return (this.DummyClassType == null) ? "WhitOutInclude" : "WhitInclude :" + this.DateField.ToString(Constants.FMT_FECHA) + " - " + this.DummyClassType.ClassType + " - " + this.DecimalField.ToString(Constants.FMT_DECIMAL_N2);
            }
        }


        #endregion
    }
}
