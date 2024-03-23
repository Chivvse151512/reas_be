namespace BusinessObject
{
    public partial class Property
    {
        public Property()
        {
            Bids = new HashSet<Bid>();
            Deposits = new HashSet<Deposit>();
            PropertyFiles = new HashSet<PropertyFile>();
            PropertyImages = new HashSet<PropertyImage>();
            PropertyTransactions = new HashSet<PropertyTransaction>();
        }

        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string Address { get; set; } = null!;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal StartingPrice { get; set; }
        public decimal StepPrice { get; set; }
        public decimal CurrentPrice { get; set; }
        public int SellerId { get; set; }
        public int? CurrentWinnerId { get; set; }
        public int Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int? StaffVerifyId { get; set; }
        public string? Note { get; set; }

        public virtual User? CurrentWinner { get; set; }
        public virtual User Seller { get; set; } = null!;
        public virtual User? StaffVerify { get; set; }
        public virtual ICollection<Bid> Bids { get; set; }
        public virtual ICollection<Deposit> Deposits { get; set; }
        public virtual ICollection<PropertyFile> PropertyFiles { get; set; }
        public virtual ICollection<PropertyImage> PropertyImages { get; set; }
        public virtual ICollection<PropertyTransaction> PropertyTransactions { get; set; }
    }
}
