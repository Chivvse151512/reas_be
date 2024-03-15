using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.DTO
{
    public class Message
    {
        public string message { get; set; }

        public static Message of(string message)
        {
            return new Message { message = message };
        }  
    }
}
