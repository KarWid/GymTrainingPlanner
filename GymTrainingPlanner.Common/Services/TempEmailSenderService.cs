namespace GymTrainingPlanner.Common.Services
{
    using System.Net.Mail;
    using System.Threading.Tasks;

    public interface IEmailSenderService
    {
        Task SendAsync(string emailTo, string subject, string body);
    }

    public class TempEmailSenderService : IEmailSenderService
    {
        private readonly SmtpClient _smtpClient;

        public TempEmailSenderService(SmtpClient smtpClient)
        {
            _smtpClient = smtpClient;
        }

        public async Task SendAsync(string emailTo, string subject, string body)
        {
            // TODO @KWidla: To change from config
            // TODO @Kwidla: Fix SMTP email - email token is not sent correctly
            var mailMessage = new MailMessage("gymTrainingPlanner@localhost", emailTo, subject, body);
            await Task.Run(() => _smtpClient.Send(mailMessage));
        }
    }
}
