using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.DTO
{
    public class UpdateStatusPropertyRequest
    {
        public int Id { get; set; }
        public int Status {  get; set; }
    }
}
