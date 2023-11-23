
namespace Naomi.promotion_service.Configurations
{
    public class AppConfig
    {
        public string? PostgreSqlConnectionString { get; set; }
        public string? KafkaConnectionString { get; set; }
        public string? EmailDomain { get; set; }
        public string? EmailHost { get; set; }
        public int EmailPort { get; set; }
        public string? TemplateRandomOtp { get; set; }
    }
}
