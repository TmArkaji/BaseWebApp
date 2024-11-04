namespace BaseWebApplication.Configurations
{
    public class Constants
    {
        public const string DEFAULT_USER_ID = @"32afb49a-7489-4e92-bdef-6ffaae987a42";
        public const string ANONYMOUSE_USER_ID = @"32afb49a-7489-4e92-bdef-6ffaae987aAA";

        public const string GESTOR_ROLE_ID = @"32a2b1b0-619e-4152-86e8-401db0259a12";
        public const string ADMIN_ROLE_ID = @"32a2145c-1e0d-4102-a4bf-043481824ceb";

        public const string DEFAULT_USER_EMAIL = @"leonardo.jrm@gmail.com";
        public const string ADMIN_ROLE = @"ADMIN";
        public const string GESTOR_ROLE = @"Gestor";

        public const string DEFAULT_PATH = "D:\\{0}\\";

        public const string GRID_PAGE_SIZE = "20";
        public const string GRID_ALLOWED_PAGE_SIZES = "[20, 40, 80]";


        #region Formato

        public const string GRID_DATE_FMT = "dd/MM/yyyy";
        public const string GRID_DATE_TYPE = "date";


        public const string FMT_FECHA = "{0:dd/MM/yyyy}";

        public const string FMT_DATETIME = "{0:dd/MM/yyyy HH:mm}";

        public const string FMT_CURRENCY = "{0:#,0.00}";

        public const string FMT_INT = "{0:n0}";

        public const string FMT_DECIMAL = "{0:n2}";
        public const string FMT_DECIMAL_N2 = "N2";

        public const string FMT_FULLDECIMAL = "{0:n5}";

        public const string FMT_PERCENT = "{0:P2}";

        #endregion

    }
}
