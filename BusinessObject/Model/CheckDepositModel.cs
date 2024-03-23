using System.ComponentModel.DataAnnotations;

namespace BusinessObject.Model
{
	public class CheckDepositModel
	{
        [Required]
        public int UserId { get; set; }

        [Required]
        public int PropertyId { get; set; }
    }
}

