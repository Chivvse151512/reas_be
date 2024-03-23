namespace BusinessObject.Model
{
    public class ForgotPasswordConfirmModel
    {
        public string Email { get; set; }
        public string Token { get; set; }
        public string NewPassword { get; set; }
    }
}
