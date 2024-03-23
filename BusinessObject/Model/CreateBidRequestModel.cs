using System.ComponentModel.DataAnnotations;

namespace BusinessObject.Model;
public class CreateBidRequestModel
{
    public int UserId { get; set; }
    public int PropertyId { get; set; }
    [Required(ErrorMessage = "Amount is required.")]
    [Range(0.01, double.MaxValue, ErrorMessage = "Amount must be greater than 0.")]
    public decimal Amount { get; set; }
}