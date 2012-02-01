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

        private readonly string host;

        private readonly int port;

        private static SmtpClient client;

        private MailSender() 
        {
            host = Properties.Resources.SmtpServer;
            port = 587;
            client = new SmtpClient(host, port);
        }

        public static MailSender Instance()
        {
            if (instance == null)
                instance = new MailSender();

            return instance;
        }

        public bool SendMessage(string login, string password, MailMessage message)
        {
            client.EnableSsl = true;
            client.Credentials = new NetworkCredential(login, password);
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
