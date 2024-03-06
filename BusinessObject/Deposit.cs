using System;
using System.Collections.Generic;

namespace BusinessObject
{
    public partial class Deposit
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int PropertyId { get; set; }
        public decimal Amount { get; set; }
        public int Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public virtual Property Property { get; set; } = null!;
    }
}
