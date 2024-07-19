using DeparProject.Configure;
using DeparProject.Events;
using DeparProject.Interfaces;
using Microsoft.AspNetCore.Identity.UI.Services;
using System.Net.Mail;
using System.Net;
using Microsoft.Extensions.Options;

namespace DeparProject.Services
{
    public class EmailService : IEmailServiceSender
    {
        private readonly EmailSettings _emailSettings;

        public EmailService(IEventBus eventBus, IOptions<EmailSettings> emailSettings)
        {
            _emailSettings = emailSettings.Value;
            eventBus.Subscribe<DemartMentEmailService>(HandleDepartmentCreation);
            eventBus.Subscribe<DemartMentEmailService>(HandleDepartmentDelete);
        }

        // Event handling method
        public void HandleDepartmentCreation(DemartMentEmailService @event)
        {
            // Replace this with your actual email sending logic
            Console.WriteLine($"Department {@event.DepartmentName} Created");
            // Handle async work inside the void method
            _ = SendEmailNotification(@event.DepartmentName, $"Department  {@event.DepartmentName} created");
        }

        public void HandleDepartmentDelete(DemartMentEmailService @event)
        {
            // Replace this with your actual email sending logic
            Console.WriteLine($"Department {@event.DepartmentName} Created");
            // Handle async work inside the void method
            _ = SendEmailNotification(@event.DepartmentName, $"Department  {@event.DepartmentName} Deleted");
        }

        public async Task SendEmailNotification(string departmentName, string Message)
        {
            var fromAddress = new MailAddress("mostafaihab2019@gmail.com", "MostafaIhab");
            var toAddress = new MailAddress("mostafaihab2019@gmail.com", "MostafaIhab");
            const string subject = "New Department Created";
            const string body = "A new department has been created."; // Add body content here

            // Create the SmtpClient object with correct SMTP server settings
            using (var smtpClient = new SmtpClient("smtp.gmail.com", 587))
            {
                smtpClient.Credentials = new NetworkCredential("mostafaihab2019@gmail.com", "Email Password");
                smtpClient.EnableSsl = true; // Use SSL for secure transmission

                // Create the MailMessage object
                using (var mail = new MailMessage())
                {
                    mail.From = fromAddress;
                    mail.To.Add(toAddress);
                    mail.Subject = subject;
                    mail.Body = body; // Set the body of the email

                    // Send the email asynchronously
                    await smtpClient.SendMailAsync(mail);
                }
            }
        }
    }
}
