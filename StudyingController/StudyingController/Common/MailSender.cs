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
    static class MailSender
    {
       // private string smtpHost;// = "smtp.gmail.com";
       // private int smtpPort;// = 587;
       // private string login;// = "maksim.pizhov";
       // private string password;// = "maksim17111991pizhov";
        static private SmtpClient client;
        static public bool SendMessage(string host, int port, string login, string password,string from, string to, string subject, string text)
        {
            client = new SmtpClient(host, port);
            client.EnableSsl = true;
            client.Credentials = new NetworkCredential(login, password);
            MailMessage message = new MailMessage(from, to, subject, text);
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
