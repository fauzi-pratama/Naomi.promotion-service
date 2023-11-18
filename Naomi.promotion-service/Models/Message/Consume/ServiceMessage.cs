
namespace Naomi.promotion_service.Models.Message.Consume
{
    public class ServiceMessage
    {
        public string? DocumentNumber { get; set; }
        public string? SyncName { get; set; }
        public object? SyncData { get; set; }
    }
}
