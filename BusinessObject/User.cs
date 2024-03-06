using System;
using System.Collections.Generic;

namespace BusinessObject
{
    public partial class User
    {
        public User()
        {
            Bids = new HashSet<Bid>();
            PropertySellers = new HashSet<Property>();
            PropertyTransactionBuyers = new HashSet<PropertyTransaction>();
            PropertyTransactionSellers = new HashSet<PropertyTransaction>();
            PropertyVerifyByNavigations = new HashSet<Property>();
            Transactions = new HashSet<Transaction>();
        }

        public int Id { get; set; }
        public string UserName { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string FullName { get; set; } = null!;
        public string? Address { get; set; }
        public string? Phone { get; set; }
        public string? Avatar { get; set; }
        public int RoleId { get; set; }
        public int Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public virtual Role Role { get; set; } = null!;
        public virtual ICollection<Bid> Bids { get; set; }
        public virtual ICollection<Property> PropertySellers { get; set; }
        public virtual ICollection<PropertyTransaction> PropertyTransactionBuyers { get; set; }
        public virtual ICollection<PropertyTransaction> PropertyTransactionSellers { get; set; }
        public virtual ICollection<Property> PropertyVerifyByNavigations { get; set; }
        public virtual ICollection<Transaction> Transactions { get; set; }
    }
}
