using Demo.DAL.Data.Models;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Demo.PL.Helpers
{
    public  static class EmailSettings
    {
        public static  void SendMailAsync(Email mail)
        {
            var client = new SmtpClient("stmp.gmail.com", 587);
            client.EnableSsl = true;
            client.Credentials = new NetworkCredential("ahmed50027@gmail.com", "ahjlohfghjkl");
             client.Send("ahmed50027@gmail.com", mail.Recipients, mail.Subject, mail.Body);

        }

    }
}
