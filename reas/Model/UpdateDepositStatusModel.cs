using System.ComponentModel.DataAnnotations;

namespace reas.Model
{
	public class UpdateDepositStatusModel
	{
        [Required]
        public int NewStatus { get; set; }
    }
}

