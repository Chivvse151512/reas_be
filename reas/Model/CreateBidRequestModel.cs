using System.ComponentModel.DataAnnotations;

namespace reas.Model;
public class CreateBidRequestModel
{
    [Required]
    public int UserId { get; set; }

    [Required]
    public int PropertyId { get; set; }

    [Required]
    [Range(0.01, double.MaxValue, ErrorMessage = "Amount must be greater than 0.")]
    public decimal Amount { get; set; }
}