using System;
using System.Collections.Generic;

namespace BusinessObject
{
    public partial class PropertyImage
    {
        public int Id { get; set; }
        public int PropertyId { get; set; }
        public string Image { get; set; } = null!;

        public virtual Property Property { get; set; } = null!;
    }
}
