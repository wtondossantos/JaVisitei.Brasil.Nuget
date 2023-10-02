namespace JaVisitei.Brasil.Business.Constants
{
    public static class Constant
    {
        public const int CONFIRMATION_CODE_EXPIRATION_TIME_EMAIL = 30;
        public const int CONFIRMATION_CODE_EXPIRATION_TIME_PASSWORD = 15;
        public const double REDIS_LOW_FREQUENCY = 525960;
        public const double REDIS_MODERATE_FREQUENCY = 43800;
        public const double REDIS_HIGH_FREQUENCY = 1440;
        public const string ID_COUNTRY_BRAZIL = "bra_brasil";
        public const string REGEX_EXPRESSION_RGB = @"^(\s*(?:(?:\d{1}|1\d\d|2(?:[0-4]\d|5[0-5]))*,?){3})$";
        public const string REGEX_EXPRESSION_DATE = @"^\d{4}[\/\-](0?[1-9]|1[012])[\/\-](0?[1-9]|[12][0-9]|3[01])$";
        public const string REGEX_EXPRESSION_EMAIL = @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$";
        public const string REGEX_EXPRESSION_MANAGER_CODE = @"^[A-Za-z\d]{8}$";
        public const string REGEX_EXPRESSION_MANAGER_CODE_FULL = @"^[A-Za-z\d]{8}[\d]{2,}$";
        public const string REGEX_EXPRESSION_ONLY_NUMBER = @"^[\d ]+$";
        public const string REGEX_EXPRESSION_ONLY_TEXT = @"^[A-Za-záàâãéèêíïóôõöúçñÁÀÂÃÉÈÍÏÓÔÕÖÚÇÑ ]+$";
        public const string REGEX_EXPRESSION_PASSWORD = @"^(?=.*[A-Z])(?=.*[a-z])(?=.*\d)[A-Za-z\d\W]{8,50}$";
        public const string REGEX_EXPRESSION_REGION_ID = @"^(?=.*[a-z_])[a-z_]{1,50}$";
        public const string REGEX_EXPRESSION_UNSERNAME = @"^[A-Za-z\d_]{3,50}$";
    }
}
