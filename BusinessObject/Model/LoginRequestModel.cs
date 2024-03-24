using System.ComponentModel.DataAnnotations;
using BusinessObject.ValidateAttributes;

namespace BusinessObject.Model
{
    public class LoginRequestModel
    {
        [Required(ErrorMessage = "Tên đăng nhập không được để rỗng !")]
        //[RequiredUsername(Value = "CDF", ErrorMessage = "Tên đăng nhập không hợp lệ")]
		//[EmailAddress(ErrorMessage = "Tên đăng nhập phải có định dạng Email")]
		public string? UserName { get; set; }

        [Required(ErrorMessage = "Mật khẩu không được để trống !")]
        public string? Password { get; set; }
    }
}
