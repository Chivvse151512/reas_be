using System.ComponentModel.DataAnnotations;

namespace BusinessObject.Model
{
	public class UpdateDepositStatusModel
	{
        [Required]
        public int NewStatus { get; set; }
    }
}

