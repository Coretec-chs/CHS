using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Web.Configuration;

namespace WebUI.Utility
{
    public class Email
    {
        private string from, to, subject, body;
        SmtpClient emailClient;

        public Email()
        {
            try
            {
                string smtp = WebConfigurationManager.AppSettings["SMTP"];
                int port = int.Parse(WebConfigurationManager.AppSettings["Port"]);
                bool ssl = WebConfigurationManager.AppSettings["EnableSSL"] == "true" ? true : false;
                this.from = WebConfigurationManager.AppSettings["SenderEmail"];
                string password = WebConfigurationManager.AppSettings["SenderPassword"];
                //this.emailClient = new SmtpClient("localhost", 8989);
                //this.emailClient.UseDefaultCredentials = true;

                this.emailClient = new SmtpClient(smtp, port);
                this.emailClient.Credentials = new NetworkCredential(from, password);
                this.emailClient.EnableSsl = true;

                //this.emailClient.DeliveryMethod = SmtpDeliveryMethod.Network;

                this.subject = "Engineers Board of Kenya";
                //this.from = "amuthonga.optimal@gmail.com";
                //this.from = "amuthonga.optimal@gmail.com";

            }
            catch (Exception ex)
            {
                ex.Data.Clear();
                //throw;
            }
        }

        public Email(string fromEmail, string subject)
        {
            try
            {
                //this.emailClient = new SmtpClient("localhost", 8989);
                //this.emailClient.UseDefaultCredentials = true;

                this.emailClient = new SmtpClient("192.168.1.87", 25);
                //this.emailClient.Credentials = new NetworkCredential("helbprod@helb.co.ke","Helb@2015");
                this.emailClient.UseDefaultCredentials = true;

                //this.emailClient.DeliveryMethod = SmtpDeliveryMethod.Network;

                this.subject = subject;
                //this.from = "no-reply_" + DateTime.Now.ToShortDateString() + "@helb.co.ke";
                this.from = fromEmail;

            }
            catch (Exception ex)
            {
                ex.Data.Clear();
                //throw;
            }
        }

        public string From
        {
            get
            {
                try { return this.from; }
                catch (Exception)
                {
                    throw;
                }
            }
            set
            {
                try { this.from = value; }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        public string To
        {
            get
            {
                try { return this.to; }
                catch (Exception)
                {
                    throw;
                }
            }
            set
            {
                try { this.to = value; }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        public string Subject
        {
            get
            {
                try { return this.subject; }
                catch (Exception)
                {
                    throw;
                }
            }
            set
            {
                try { this.subject = value; }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        public string Body
        {
            get
            {
                try { return this.body; }
                catch (Exception)
                {
                    throw;
                }
            }
            set
            {
                try { this.body = value; }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        public void SendMail()
        {
            try
            {
                if (this.to != "")
                {
                    MailMessage message = new MailMessage(this.from, this.to, this.subject, this.body);

                    //message.BodyEncoding = Encoding.ASCII;
                    //message.DeliveryNotificationOptions = DeliveryNotificationOptions.OnSuccess;
                    message.IsBodyHtml = true;
                    message.Priority = MailPriority.High;
                    //MailAddress ma = new MailAddress(this.from);
                    //message.From = ma;

                    //GeneralFunctions.SendEmail(this.to, this.Body);
                    ServicePointManager.ServerCertificateValidationCallback =
                        delegate(object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
                        { return true; };

                     this.emailClient.Send(message);
                }
            }
            catch (Exception ex)
            {
                ex.Data.Clear();
                //throw;
            }
        }

        public void SendMail(string attachmentPath)
        {
            try
            {
                if (this.to != "")
                {
                    MailMessage message = new MailMessage(this.from, this.to, this.subject, this.body);

                    //message.BodyEncoding = Encoding.ASCII;
                    message.DeliveryNotificationOptions = DeliveryNotificationOptions.OnSuccess;
                    message.IsBodyHtml = true;
                    message.Priority = MailPriority.High;
                    MailAddress ma = new MailAddress(this.from);
                    message.From = ma;
                    message.Attachments.Add(new Attachment(attachmentPath));

                    //GeneralFunctions.SendEmail(this.to, this.Body);

                    this.emailClient.Send(message);
                }
            }
            catch (Exception ex)
            {
                ex.Data.Clear();
                //throw;
            }
        }

    }
}