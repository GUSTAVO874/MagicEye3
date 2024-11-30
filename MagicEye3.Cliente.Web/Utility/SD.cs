namespace MagicEye3.Cliente.Web.Utility
{
    public class SD
    {
        
        public static string AuthAPIBase { get; set; } = "https://localhost:7240/";
        public static string BackEndAPIBase { get; set; } = "https://localhost:7109/";

        public const string RoleAdmin = "ADMIN";
        public const string RoleCustomer = "CUSTOMER";
        //public const string TokenCookie = "JWTToken";
        public enum ApiType 
        { 
            GET,
            POST,
            PUT,
            DELETE
        }
    }
}
