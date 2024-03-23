namespace BusinessObject.Model
{
    public class UserUpdateProfileRequestModel
    {
        public string Email { get; set; } = null!;
        public string FullName { get; set; } = null!;
        public string? Address { get; set; }
        public string? Phone { get; set; }
        public string? Avatar { get; set; }
    }
}
