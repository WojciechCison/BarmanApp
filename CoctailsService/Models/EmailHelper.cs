using MimeKit;
using MailKit.Net.Smtp;

namespace Identity.Models
{
    public class EmailHelper
    {
        public void SendEmail(string userEmail, string confirmationLink)
        {
            var message = new MimeMessage();

            var from = new MailboxAddress("Barman application", "incubatorcontrol@gmail.com");
            var to = new MailboxAddress("Client", userEmail);

            var body = new BodyBuilder();
            string emailMessage = confirmationLink;
            body.TextBody = emailMessage;

            message.From.Add(from);
            message.To.Add(to);
            message.Subject = "Confirm your email";
            message.Body = body.ToMessageBody();

            var client = new SmtpClient();
            client.Connect("smtp.gmail.com", 587, false);
            client.Authenticate("incubatorcontrol@gmail.com", "vilwalthvqbcxgyj");
            client.Send(message);
            client.Disconnect(true);
            client.Dispose();
        }
    }
}