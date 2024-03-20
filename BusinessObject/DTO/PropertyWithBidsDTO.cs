using System;
namespace BusinessObject.DTO
{
	public class PropertyWithBidsDTO
	{
        public int Id { get; set; }
        public int SellerId { get; set; }
        public int? CurrentWinner { get; set; }
        public int? VerifyBy { get; set; }
        public ICollection<Bid> Bids { get; set; }
    }
}

