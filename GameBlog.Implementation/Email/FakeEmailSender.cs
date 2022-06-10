using GameBlog.Application.Email;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameBlog.Implementation.Email
{
    public class FakeEmailSender : IEmailSender
    {
        public void SendEmail(SendEmailDto email)
        {
            Console.WriteLine("Sending email to: " + email.To);
            Console.WriteLine("Subject: " + email.Subject);
            Console.WriteLine("Body: " + email.Body);
        }
    }
}
