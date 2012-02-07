using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;

namespace StudyingController.Common
{
    class MailSender
    {  
        
        private static MailSender instance;

        private static SmtpClient client;

        private MailSender() 
        {
            client = new SmtpClient
            {
                Host = Properties.Resources.SmtpServer,
                Port = 587,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(Properties.Resources.EmailLogin, Properties.Resources.EmailPassword),
                EnableSsl = true,
                Timeout = 1
            };
            /*host = Properties.Resources.SmtpServer;
            port = 587;
            login = Properties.Resources.EmailLogin;
            password = Properties.Resources.EmailPassword;
            client = new SmtpClient(host, port);*/
        }

        public static MailSender GetInstance()
        {
            if (instance == null)
                instance = new MailSender();

            return instance;
        }

        public bool SendMessage(MailMessage message)
        {
            try
            {
                client.Send(message);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
