using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.DTO
{
    public class Message
    {
        private string message;
        private Message(string message)
        {
            this.message = message;
        }

        public static Message of(string message)
        {
            return new Message(message);
        }
    }
}
