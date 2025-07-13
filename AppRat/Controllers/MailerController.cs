using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;
using System.Net;

public class MailerController : Controller
{
    private readonly IConfiguration _configuration;

    public MailerController(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    [HttpPost]
    public ActionResult SendEmail(string receiver, string subject, string message/*, byte[] attachmentBytes, string attachmentName*/)
    {
        try
        {
            if (ModelState.IsValid)
            {
                var senderEmailConfig = "****************"; // Replace with your sender email configuration
                if (string.IsNullOrEmpty(senderEmailConfig))
                {
                    throw new ArgumentException("Sender email address cannot be null or empty", nameof(senderEmailConfig));
                }

                var senderEmail = new MailAddress(senderEmailConfig, "AppRat (Testing Only)");
                var receiverEmail = new MailAddress(receiver, receiver);
                var password = "*******************"; // Use a secure way to store and retrieve passwords
                var smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(senderEmail.Address, password)
                };

                using (var mess = new MailMessage(senderEmail, receiverEmail)
                {
                    Subject = subject,
                    Body = message,
                    IsBodyHtml = true
                })
                {
                    //if (attachmentBytes != null && attachmentBytes.Length > 0)
                    //{
                    //    var attachment = new Attachment(new MemoryStream(attachmentBytes), attachmentName);
                    //    mess.Attachments.Add(attachment);
                    //}

                    smtp.Send(mess);
                }
                return View();
            }
        }
        catch (Exception ex)
        {
            ViewBag.Error = $"Error: {ex.Message} | StackTrace: {ex.StackTrace}";
            Console.WriteLine(ex.Message);
            Console.WriteLine(ex.StackTrace);
        }
        return View();
    }

}
