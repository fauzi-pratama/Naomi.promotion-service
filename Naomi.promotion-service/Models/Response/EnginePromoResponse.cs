namespace Naomi.promotion_service.Models.Response
{
    public class EnginePromoResponse
    {
        public string? CompanyCode { get; set; }
        public string? PromoCode { get; set; }
        public string? PromoName { get; set; }
        public bool? Status { get; set; }
        public string? ExpressionEngine { get; set; }
        public List<VariableEngine>? VariableEngine { get; set; }
    }

    public class VariableEngine
    {
        public string? Name { get; set; }
        public string? Expression { get; set; }
    }
}
