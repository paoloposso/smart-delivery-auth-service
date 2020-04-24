namespace SmartDelivery.Auth.Domain.Model
{
    public class AppSettings
    {
        public string AllowedHosts { get; set; }
        public string LogLevel { get; set; }
        public string MongoDbCnnString { get; set; }
        public string JwtSecret { get; set; }
        public int JwtExpirationTimeInMinutes { get; set; }
    }
}