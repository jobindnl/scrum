namespace angular.Web.Models.DTO
{
    public class ForgotPasswordToken
    {
        public string newPwd { get; set; }
        public string confirmPwd { get; set; }
        public string Token { get; set; }
        public string Email { get; set; }
    }
}
