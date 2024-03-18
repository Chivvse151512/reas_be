using System.ComponentModel.DataAnnotations;

namespace reas.Model
{
    public class ForgotPasswordModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
