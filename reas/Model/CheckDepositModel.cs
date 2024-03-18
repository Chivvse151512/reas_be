using System.ComponentModel.DataAnnotations;

namespace reas.Model
{
	public class CheckDepositModel
	{
        [Required]
        public int UserId { get; set; }

        [Required]
        public int PropertyId { get; set; }
    }
}

