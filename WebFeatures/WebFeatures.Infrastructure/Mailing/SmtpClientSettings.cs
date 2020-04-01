namespace WebFeatures.Infrastructure.Mailing
{
    internal class SmtpClientSettings
    {
        public string Address { get; set; }
        public string Password { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }
        public bool UseSsl { get; set; }
    }
}
