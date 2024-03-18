using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace reas.Services
{
    public interface IEMailSenderService
    {
        void SendEmail(string toEmail, string subject, string messageBody);
    }
}
