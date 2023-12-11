namespace NewCityInfo.API.Services
{
    public class CloudMailService : IMailService
    {
        private readonly string _mailTo = string.Empty;
        private readonly string _mailFrom = string.Empty;
        public CloudMailService(IConfiguration configuration)
        {
            _mailTo = configuration["mailSettings:mailToAddress"];
            _mailFrom = configuration["mailSettings:mailFromAddress"];

        }
        public void send(string subject, string message)
        {
            Console.WriteLine($"mail from {_mailFrom} to mail {_mailTo} with" +
                $" {nameof(CloudMailService)}");
            Console.WriteLine($"{subject}");
            Console.WriteLine($"{message}");
        }
    }
}
