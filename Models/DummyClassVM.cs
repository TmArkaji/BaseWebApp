using BaseWebApplication.Configurations;
using BaseWebApplication.Data;
using BaseWebApplication.Resources;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BaseWebApplication.Models
{
    public class DummyClassVM : BaseEntityVM<string>
    {
        public override string ID { get; set; }
        public string Dummy { get; set; }

        [Display(Name = nameof(Resource.DummyClass_DateField), ResourceType = typeof(Resource))]
        [BindProperty, DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = false, DataFormatString = Constants.FMT_FECHA)]
        public DateTime DateField { get; set; }
        public DateTime DateTimeField { get; set; }
        public DateTime? NulableDateField { get; set; }

        [Column(TypeName = Constants.COLUMN_TYPE_DECIMAL)]
        [DisplayFormat(ApplyFormatInEditMode = false, DataFormatString = Constants.FMT_CURRENCY)]
        public decimal DecimalField { get; set; }
        public int IntField { get; set; }

        public string? TextAreaField { get; set; }

        public bool BoolField { get; set; }

        public string? Conditional { get; set; }
      
        public string? DisplayText { get; set; }


        public int DummyClassTypeID { get; set; }
        public virtual DummyClassTypeVM? DummyClassType { get; set; }
    }
}
