namespace BusinessObject.Model
{
    public class ChangePasswordRequestModel
    {
        public string UserName { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
    }
}
