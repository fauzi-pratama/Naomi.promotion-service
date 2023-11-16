using Newtonsoft.Json;

namespace Naomi.promotion_service.Models.Message.Consume
{
    public class MopMessage
    {
        [JsonProperty("sales_organization_code")]
        public string? SalesOrganizationCode { get; set; }

        [JsonProperty("site_code")]
        public string? SiteCode { get; set; }

        [JsonProperty("mop_code")]
        public string? MopCode { get; set; }

        [JsonProperty("mop_name")]
        public string? MopName { get; set; }
    }
}
