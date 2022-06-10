using GameBlog.Application.Email;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace GameBlog.Implementation.Email
{
    public class SmtpEmailSender : IEmailSender
    {
        private readonly string _email;
        private readonly string _password;

        public SmtpEmailSender(string email, string password)
        {
            _email = email;
            _password = password;
        }

        public void SendEmail(SendEmailDto dto)
        {
            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                Credentials = new NetworkCredential(_email,_password),
                UseDefaultCredentials = false
                //ime i pass maila sa kog saljemo
            };

            var message = new MailMessage(_email, dto.To);
            message.Subject = dto.Subject;
            message.Body = dto.Body;
            message.IsBodyHtml = true;
            smtp.Send(message);
        }
    }
}
