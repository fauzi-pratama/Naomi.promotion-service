
using AutoMapper;
using Naomi.promotion_service.Models.Dto;
using Naomi.promotion_service.Models.Request;
using Naomi.promotion_service.Models.Response;

namespace Naomi.promotion_service.Configurations
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<FindPromoRequest, ParamsPromoDto>();
            CreateMap<RulesEngine.Models.RuleResultTree, ResultPromoDto>();
            CreateMap<ResultPromoDto, FindPromoResponse>();
        }
    }
}
