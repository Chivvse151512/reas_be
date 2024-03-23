namespace BusinessObject
{
    public partial class PropertyTransaction
    {
        public int Id { get; set; }
        public int PropertyId { get; set; }
        public int SellerId { get; set; }
        public int BuyerId { get; set; }
        public decimal Amount { get; set; }
        public int Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public virtual User Buyer { get; set; } = null!;
        public virtual Property Property { get; set; } = null!;
        public virtual User Seller { get; set; } = null!;
    }
}
