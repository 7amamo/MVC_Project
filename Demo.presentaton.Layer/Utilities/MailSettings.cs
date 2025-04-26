using System.Net;
using System.Net.Mail;

namespace Demo.presentaton.Layer.Utilities
{
	public class EMail
	{
		public string Subject { get; set; }
		public string Body { get; set; }
		public string To { get; set; }
	}
	public class MailSettings
	{
		public static void  SendEmail(EMail email)
		{
			var client = new SmtpClient("smtp.gmail.com",587);
			client.EnableSsl = true;
			client.Credentials = new NetworkCredential("ahmedhamamo095@gmail.com", "olwemyfvlydskctl");


			client.Send("ahmedhamamo095@gmail.com", email.To, email.Subject, email.Body);				
        }
	}

}
