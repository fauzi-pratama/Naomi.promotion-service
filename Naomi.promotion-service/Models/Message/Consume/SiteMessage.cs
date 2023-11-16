
using Newtonsoft.Json;

namespace Naomi.promotion_service.Models.Message.Consume
{
    public class SiteMessage
    {
        [JsonProperty("company_code")]
        public string? CompanyCode { get; set; }

        [JsonProperty("company_description")]
        public string? CompanyDescription { get; set; }

        [JsonProperty("site_code")]
        public string? SiteCode { get; set; }

        [JsonProperty("site_description")]
        public string? SiteDescription { get; set; }

        [JsonProperty("pricing_zone")]
        public string? PricingZone { get; set; }
    }
}
