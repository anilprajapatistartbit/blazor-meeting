using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingSchedulingApp.Application.SmtpService
{
    public interface IEmailService
    {
        Task<bool> SendEmailAsync(EmailMessage email);
    }
}
