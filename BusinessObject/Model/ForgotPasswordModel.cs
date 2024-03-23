using System.ComponentModel.DataAnnotations;

namespace BusinessObject.Model
{
    public class ForgotPasswordModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
