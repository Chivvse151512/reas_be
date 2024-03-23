using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.DTO
{
    public class UpdatePricePropertyRequest
    {
        public int Id { get; set; }
        public decimal Price { get; set; }
    }
}
