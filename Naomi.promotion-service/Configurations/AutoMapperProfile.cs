
using AutoMapper;
using Naomi.promotion_service.Models.Dto;
using Naomi.promotion_service.Models.Request;
using Naomi.promotion_service.Models.Entities;
using Naomi.promotion_service.Models.Response;

namespace Naomi.promotion_service.Configurations
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<ItemProduct, ItemProductDto>();
            CreateMap<FindPromoRequest, ParamsPromoDto>();
            CreateMap<RulesEngine.Models.RuleResultTree, ResultPromoDto>();
            CreateMap<PromoMopRequireDetailDto, PromoMopRequireDetail>();
            CreateMap<PromoMopRequireDto, PromoMopRequire>();
            CreateMap<PromoListItemDetailDto, PromoListItemDetail>();
            CreateMap<PromoListItemDto, PromoListItem>();
            CreateMap<ResultPromoDto, FindPromoResponse>();
            CreateMap<FindPromoShowRequest, ParamsFindPromoWithoutEngineDto>();
            CreateMap<FindPromoRedeemRequest, ParamsFindPromoWithoutEngineDto>();
            CreateMap<PromoRuleResult, ResultFindPromoWithoutEngineDetailDto>()
                .ForMember(x => x.SkuCode, opt => opt.MapFrom(q => q.Item))
                .ForMember(x => x.PromoValue, opt => opt.MapFrom(q => q.DscValue))
                .ForMember(x => x.PromoValueMax, opt => opt.MapFrom(q => q.MaxValue))
                .ForMember(x => x.LinkConnection, opt => opt.MapFrom(q => q.LinkRsl));
            CreateMap<PromoRule, ResultFindPromoWithoutEngineDto>()
                .ForMember(x => x.PromoCode, opt => opt.MapFrom(q => q.Code))
                .ForMember(x => x.PromoName, opt => opt.MapFrom(q => q.Name))
                .ForMember(x => x.PromoType, opt => opt.MapFrom(q => q.PromoActionType))
                .ForMember(x => x.PromoTypeResult, opt => opt.MapFrom(q => q.ItemType))
                .ForMember(x => x.ValDiscount, opt => opt.MapFrom(q => q.PromoActionValue))
                .ForMember(x => x.ValMaxDiscount, opt => opt.MapFrom(q => q.MaxValue))
                .ForMember(x => x.PromoCls, opt => opt.MapFrom(q => q.Cls))
                .ForMember(x => x.PromoDesc, opt => opt.MapFrom(q => q.PromoDesc))
                .ForMember(x => x.PromoTermCondition, opt => opt.MapFrom(q => q.PromoTermCondition))
                .ForMember(x => x.PromoImageLink, opt => opt.MapFrom(q => q.PromoImageLink))
                .ForMember(x => x.ResultDetail, opt => opt.MapFrom(q => q.PromoRuleResult));
            CreateMap<ResultFindPromoWithoutEngineDto, FindPromoShowResponse>();
            CreateMap<ResultFindPromoWithoutEngineDto, FindPromoRedeemResponse>();

        }
    }
}
