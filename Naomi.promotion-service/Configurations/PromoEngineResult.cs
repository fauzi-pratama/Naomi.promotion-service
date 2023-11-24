
using Newtonsoft.Json;
using RulesEngine.Models;
using RulesEngine.Actions;
using Naomi.promotion_service.Models.Dto;
using Naomi.promotion_service.Models.Entities;

namespace Naomi.promotion_service.Configurations
{
    public class PromoEngineResult : ActionBase
    {
        public override async ValueTask<object> Run(ActionContext context, RuleParameter[] ruleParameters)
        {
            //Get Data Cart
            ParamsPromoDto? dataCart = JsonConvert.DeserializeObject<ParamsPromoDto>(
                                        JsonConvert.SerializeObject(
                                            ruleParameters.Where(q => q.Name == "paramsPromo").Select(q => q.Value).FirstOrDefault()
                                            )
                                        );

            //Get Data Promo Result
            var dataPromo = JsonConvert.DeserializeObject<PromoRule>(context.GetContext<string>("datapromo"));

            //Return Calculate per Promo
            return await CalculatePromoSingleAsync(dataPromo!, dataCart!);
        }

        public static async Task<ResultPromoDto> CalculatePromoSingleAsync(PromoRule dataPromo, ParamsPromoDto dataCart)
        {
            int linePromoCount = 1;
            List<PromoListItemDto> responseDetail = new(); //Variable untuk save data detail item semua group

            //Execute Promo Untuk Semua Item
            if (dataPromo.ItemType == "ALL")
            {

                List<PromoListItemDetailDto> responseDetailGroup = new();

                // Looping Item di Cart untuk Execute Promo
                foreach (var loopItemCart in dataCart.ItemProduct!)
                {

                    decimal discountTypeAll = 0;

                    if (dataPromo.PromoActionType == "AMOUNT")
                    {
                        //Execute Promo Amount

                        if (dataPromo.Cls > 1)
                        {
                            //Execute Promo Cart & Additional

                            //Get Total Harga Per SKU
                            decimal totalPriceSku = Convert.ToDecimal(loopItemCart.Qty) * loopItemCart.Price;

                            //Get Total Harga All Cart
                            decimal totalPriceCart = dataCart.ItemProduct.Sum(q => q.Price * Convert.ToDecimal(q.Qty));

                            //Get Discount Prorate di Cart
                            discountTypeAll = Math.Floor(totalPriceSku / totalPriceCart * Convert.ToDecimal(dataPromo.PromoActionValue));
                        }
                        else
                        {
                            //Execute Promo Item
                            discountTypeAll = Convert.ToDecimal(dataPromo.PromoActionValue);
                        }
                    }

                    if (dataPromo.PromoActionType == "PERCENT")
                    {
                        //Execute Promo Percent

                        discountTypeAll = Convert.ToDecimal(loopItemCart.Qty) * loopItemCart.Price * Convert.ToDecimal(dataPromo.PromoActionValue!.Replace("%", "")) / 100;
                    }

                    PromoListItemDetailDto ResponseDetailGroupSingle = new()
                    {
                        LineNo = loopItemCart.LineNo,
                        SkuCode = loopItemCart.SkuCode,
                        ValDiscount = dataPromo.PromoActionValue,
                        ValMaxDiscount = dataPromo.MaxValue,
                        Price = loopItemCart.Price,
                        Qty = loopItemCart.Qty,
                        TotalPrice = loopItemCart.Price * Convert.ToDecimal(loopItemCart.Qty),
                        TotalDiscount = discountTypeAll,
                        TotalAfter = (loopItemCart.Price * Convert.ToDecimal(loopItemCart.Qty)) - discountTypeAll
                    };

                    responseDetailGroup.Add(ResponseDetailGroupSingle);
                }

                //Declare for Result Detail Promo
                int roundingAllItem = dataPromo.PromoActionType == "AMOUNT" && dataPromo.Cls > 1 ?
                                Convert.ToInt32(dataPromo.PromoActionValue) - Convert.ToInt32(responseDetailGroup.Sum(q => q.TotalDiscount))
                                : 0;
                decimal? total_beforeAllItem = dataCart.ItemProduct.Sum(q => Convert.ToDecimal(q.Qty) * q.Price);
                decimal? total_discountAllItem = responseDetailGroup.Sum(q => q.TotalDiscount) + Convert.ToDecimal(roundingAllItem);
                decimal? total_afterAllItem = total_beforeAllItem - total_discountAllItem;

                PromoListItemDto responseDetailSingle = new()
                {
                    LinePromo = linePromoCount,
                    Rounding = roundingAllItem,
                    TotalBefore = total_beforeAllItem,
                    TotalDiscount = total_discountAllItem,
                    TotalAfter = total_afterAllItem,
                    PromoListItemDetail = responseDetailGroup
                };

                responseDetail.Add(responseDetailSingle);
            }
            //Execute Promo Untuk Custom Item
            else if (dataPromo.ItemType == "CUSTOM")
            {

                //Grouping item bedasarkan group di db ms_promo_rule_result
                List<List<ItemGroupResultPerPromoDto>> listItemPerPromo = new(); //Variable untuk menampung item custom group
                var groupList = dataPromo.PromoRuleResult!.Select(q => q.GroupLine).Distinct().ToList(); //get data disctinc group

                //Get Data Qty Total Bundle
                double totalQtyItemBundle = 0;
                decimal totalPriceItemBudle = 0;
                if (dataPromo.PromoActionType == "BUNDLE")
                {
                    List<string> itemGetDiscountBundle = new();

                    foreach (var loopItem in dataPromo.PromoRuleResult!)
                    {
                        itemGetDiscountBundle.Add(loopItem.Item!);
                        totalPriceItemBudle += (dataCart.ItemProduct!.Find(q => q.SkuCode == loopItem.Item)!.Price *
                            Convert.ToDecimal(loopItem.DscValue));
                    }

                    totalQtyItemBundle = dataPromo.PromoRuleResult!.Sum(q => Convert.ToDouble(q.DscValue));
                }

                //Looping group untuk mendapatkan item
                foreach (var loopGroup in groupList)
                {
                    List<ItemGroupResultPerPromoDto> listItemPerGroup = new(); //Varibel untuk menampung item di 1 group
                    var listItemGroup = dataPromo.PromoRuleResult!.Where(q => q.GroupLine == loopGroup).ToList(); //get data item sesuai looping group

                    foreach (var loopItemGroup in listItemGroup)
                    {
                        //Input data item ke list dalam 1 group
                        ItemGroupResultPerPromoDto ItemPerPromo = new()
                        {
                            SkuCode = loopItemGroup.Item,
                            Value = loopItemGroup.DscValue,
                            MaxValue = loopItemGroup.MaxValue
                        };

                        listItemPerGroup.Add(ItemPerPromo);
                    }

                    listItemPerPromo.Add(listItemPerGroup);
                }

                //Looping item per group
                foreach (var loopItemperPromo in listItemPerPromo)
                {

                    List<PromoListItemDetailDto> responseDetailGroup = new(); //Model for save hasil promo item per group

                    decimal discountTypeCustom = 0;
                    List<string> dataListItem = new();

                    foreach (var loopGetDataItemPerPromo in loopItemperPromo)
                    {
                        dataListItem.Add(loopGetDataItemPerPromo.SkuCode!);
                    }

                    /* Start Untuk Cek Item AND Jika tidak ada salah satu maka tidak dapat promo */
                    List<string> dataItemGroup = new();

                    foreach (var loopItemGroupString in loopItemperPromo)
                    {
                        dataItemGroup.Add(loopItemGroupString.SkuCode!);
                    }

                    //Untuk Cek Custom Item Dan Harus Terdapat Semua Item
                    bool executePromoCekResultType = false;
                    var cekItemGroupInCart = dataCart.ItemProduct!.Where(q => dataItemGroup.Contains(q.SkuCode!));

                    executePromoCekResultType = cekItemGroupInCart.Count() == loopItemperPromo.Count || dataPromo.ResultType == "V2";

                    if (executePromoCekResultType)
                    {
                        foreach (var loopItemPerGroup in loopItemperPromo)
                        {
                            PromoListItemDetailDto responseDetailGroupSingle = new();
                            var dataItemCart = dataCart.ItemProduct!.Find(q => q.SkuCode == loopItemPerGroup.SkuCode); //Get data di cart

                            if (dataItemCart != null)
                            {

                                if (dataPromo.PromoActionType == "AMOUNT")
                                { //Execute Promo Amount
                                    if (dataPromo.Cls > 1)
                                    {
                                        //Execute Untuk Class Selain Item
                                        //Get Total Harga Per SKU
                                        decimal totalPriceSku = Convert.ToDecimal(dataItemCart.Qty) * dataItemCart.Price;

                                        //Get Total Harga Custom Item
                                        decimal totalPriceCart = dataCart.ItemProduct!.Where(q => dataListItem.Contains(q.SkuCode!))
                                            .Sum(q => q.Price * Convert.ToDecimal(q.Qty));

                                        //Get Discount Prorate di Cart Untuk Custom Item
                                        discountTypeCustom = Math.Floor(totalPriceSku / totalPriceCart * Convert.ToDecimal(dataPromo.PromoActionValue));
                                    }
                                    else
                                    {
                                        //Execute Untuk Class Item
                                        discountTypeCustom = Convert.ToDecimal(loopItemPerGroup.Value);
                                    }
                                }
                                else if (dataPromo.PromoActionType == "PERCENT")
                                { //Execute Promo Percent
                                    discountTypeCustom = dataItemCart.Price * Convert.ToDecimal(dataItemCart.Qty) *
                                        Convert.ToDecimal(loopItemPerGroup.Value!.Replace("%", "")) / 100;
                                }
                                else if (dataPromo.PromoActionType == "ITEM")
                                { //Execute Promo Item
                                    discountTypeCustom = dataItemCart.Price * dataPromo.MultipleQty;
                                }
                                else if (dataPromo.PromoActionType == "SP")
                                { //Execute Promo Special Price
                                    discountTypeCustom = (dataItemCart.Price - Convert.ToDecimal(loopItemPerGroup.Value)) *
                                        Convert.ToDecimal(dataItemCart.Qty);
                                }
                                else if (dataPromo.PromoActionType == "BUNDLE")
                                {
                                    discountTypeCustom = Math.Floor((Convert.ToDecimal(loopItemPerGroup.Value) /
                                        (decimal)totalQtyItemBundle) * (Convert.ToDecimal(totalPriceItemBudle) - Convert.ToDecimal(dataPromo.PromoActionValue)));
                                }
                                else
                                {
                                    discountTypeCustom = 0;
                                }

                                responseDetailGroupSingle.LineNo = dataItemCart.LineNo;
                                responseDetailGroupSingle.SkuCode = dataItemCart.SkuCode;
                                responseDetailGroupSingle.ValDiscount = loopItemPerGroup.Value;
                                responseDetailGroupSingle.ValMaxDiscount = loopItemPerGroup.MaxValue;
                                responseDetailGroupSingle.Price = dataItemCart.Price;
                                responseDetailGroupSingle.Qty = dataItemCart.Qty;
                                responseDetailGroupSingle.TotalPrice = dataItemCart.Price * Convert.ToDecimal(dataItemCart.Qty);
                                responseDetailGroupSingle.TotalDiscount = discountTypeCustom;
                                responseDetailGroupSingle.TotalAfter = (dataItemCart.Price * Convert.ToDecimal(dataItemCart.Qty)) - discountTypeCustom;

                                responseDetailGroup.Add(responseDetailGroupSingle);
                            }
                        }
                    }

                    if (responseDetailGroup.Count > 0)
                    {
                        //Declare for Result Detail Promo

                        int roundingAllItem = 0;
                        if (dataPromo.PromoActionType == "AMOUNT" && dataPromo.Cls > 1)
                        {
                            roundingAllItem = Convert.ToInt32(dataPromo.PromoActionValue) - Convert.ToInt32(responseDetailGroup.Sum(q => q.TotalDiscount));
                        }
                        else if (dataPromo.PromoActionType == "BUNDLE")
                        {
                            roundingAllItem = Convert.ToInt32(totalPriceItemBudle) - Convert.ToInt32(dataPromo.PromoActionValue) - Convert.ToInt32(responseDetailGroup.Sum(q => q.TotalDiscount));
                        }

                        decimal? total_beforeAllItem = dataCart.ItemProduct!.Sum(q => Convert.ToDecimal(q.Qty) * q.Price);
                        decimal? total_discountAllItem = responseDetailGroup.Sum(q => q.TotalDiscount) + Convert.ToDecimal(roundingAllItem);
                        decimal? total_afterAllItem = total_beforeAllItem - total_discountAllItem;

                        PromoListItemDto responseDetailSingle = new()
                        {
                            LinePromo = linePromoCount,
                            Rounding = roundingAllItem,
                            TotalBefore = total_beforeAllItem,
                            TotalDiscount = total_discountAllItem,
                            TotalAfter = total_afterAllItem,
                            PromoListItemDetail = responseDetailGroup.OrderBy(q => q.LineNo).ToList()
                        };

                        responseDetail.Add(responseDetailSingle);

                        linePromoCount += 1;
                    }
                }
            }

            //Varibale Save data to Response
            ResultPromoDto response = new()
            {
                TransId = dataCart.TransId,
                CompanyCode = dataCart.CompanyCode,
                PromoCode = dataPromo.Code,
                PromoName = dataPromo.Name,
                PromoType = dataPromo.PromoActionType,
                PromoTypeResult = dataPromo.ItemType,
                ValDiscount = dataPromo.PromoActionValue,
                ValMaxDiscount = dataPromo.MaxValue,
                PromoCls = dataPromo.Cls,
                PromoLvl = dataPromo.Lvl,
                MaxMultiple = dataPromo.MaxMultiple,
                MaxUse = dataPromo.MaxUse,
                MaxBalance = dataPromo.MaxBalance,
                MultipleQty = dataPromo.MultipleQty,
                PromoDesc = dataPromo.PromoDesc,
                PromoTermCondition = dataPromo.PromoTermCondition,
                PromoImageLink = dataPromo.PromoImageLink,
                PromoListItem = responseDetail.OrderByDescending(q => q.TotalDiscount).ToList()
            };

            //Save Data Require MOP
            if (dataPromo.Cls == 3 && dataPromo.PromoRuleMop != null && dataPromo.PromoRuleMop.Count > 0)
            {
                List<PromoMopRequireDetailDto> listPromoMopRequireDetail = new();

                foreach (var loopReqMop in dataPromo.PromoRuleMop!)
                {
                    PromoMopRequireDetailDto promoMopRequireDetail = new()
                    {
                        MopGroupCode = loopReqMop.MopGroupCode,
                        MopGroupName = loopReqMop.MopGroupName
                    };

                    listPromoMopRequireDetail.Add(promoMopRequireDetail);
                }

                PromoMopRequireDto? promoMopRequire = new()
                {
                    MopPromoSelectionCode = dataPromo.PromoRuleMop!.FirstOrDefault()!.MopPromoSelectionCode,
                    MopPromoSelectionName = dataPromo.PromoRuleMop!.FirstOrDefault()!.MopPromoSelectionName,
                    PromoMopRequireDetail = listPromoMopRequireDetail
                };

                response.PromoMopRequire = promoMopRequire;
            }

            //Execute Maximal Item Jika Promo Percentage
            if (response.PromoType == "PERCENT")
            {
                bool cekMaxPromoStatus = false;

                //Class Item
                if (response.PromoCls == 1)
                {
                    foreach (var loopPromoList in response.PromoListItem)
                    {
                        foreach (var loopPromoListDetail in loopPromoList.PromoListItemDetail!)
                        {
                            if (loopPromoListDetail.ValMaxDiscount != null && loopPromoListDetail.ValMaxDiscount != "" && loopPromoListDetail.ValMaxDiscount != "0"
                                && loopPromoListDetail.TotalDiscount > Convert.ToDecimal(loopPromoListDetail.ValMaxDiscount))
                            {
                                loopPromoListDetail.TotalDiscount = Convert.ToDecimal(loopPromoListDetail.ValMaxDiscount);
                                loopPromoListDetail.TotalAfter = loopPromoListDetail.TotalPrice - loopPromoListDetail.TotalDiscount;
                                cekMaxPromoStatus = true;
                            }
                        }

                        loopPromoList.TotalDiscount = loopPromoList.PromoListItemDetail.Sum(q => q.TotalDiscount);
                        loopPromoList.TotalAfter = loopPromoList.TotalBefore - loopPromoList.TotalDiscount;
                    }

                    response.ValMaxDiscountStatus = cekMaxPromoStatus;
                }
                //Class Cart
                else if ((response.PromoCls == 2 || response.PromoCls == 3) &&
                    response.ValMaxDiscount != null && response.ValMaxDiscount != "")
                {
                    //Bongkar List dan Cari Total Discount yang lebih besar dari Max Discount
                    foreach (var loopDetailPromo in response.PromoListItem)
                    {
                        //Cek Total Disctount melebihi Max Discount
                        if (loopDetailPromo.TotalDiscount > Convert.ToDecimal(response.ValMaxDiscount))
                        {
                            foreach (var loopDetailPromoItem in loopDetailPromo.PromoListItemDetail!)
                            {
                                //Execute Promo Cart & Additional

                                //Get Total Harga Per SKU
                                decimal? totalPriceSku = Convert.ToDecimal(loopDetailPromoItem.Qty) * loopDetailPromoItem.Price;

                                //Get Total Harga All Cart
                                decimal? totalPriceCart = loopDetailPromo.PromoListItemDetail.Sum(q => q.Price * Convert.ToDecimal(q.Qty));

                                //Get Discount Prorate di Cart
                                decimal? totalDiscountCovert = totalPriceSku / totalPriceCart * Convert.ToDecimal(response.ValMaxDiscount);

                                loopDetailPromoItem.TotalDiscount = Math.Floor(totalDiscountCovert ?? 0);
                                loopDetailPromoItem.TotalAfter = totalPriceSku - Math.Floor(totalDiscountCovert ?? 0);
                            }

                            //ReCalculate Convert Amount
                            loopDetailPromo.TotalDiscount = loopDetailPromo.PromoListItemDetail.Sum(q => q.TotalDiscount);
                            loopDetailPromo.TotalAfter = loopDetailPromo.PromoListItemDetail.Sum(q => q.TotalAfter);
                            loopDetailPromo.Rounding = Convert.ToInt32(response.ValMaxDiscount) - (Int32)loopDetailPromo.TotalDiscount!;
                            loopDetailPromo.TotalDiscount += loopDetailPromo.Rounding;
                            loopDetailPromo.TotalAfter -= loopDetailPromo.Rounding;
                            cekMaxPromoStatus = true;
                        }
                    }

                    response.ValMaxDiscountStatus = cekMaxPromoStatus;
                }
            }

            //Handle Message Asyn
            await Task.Run(() =>
            {
                Task.Delay(1).Wait();
            });

            return response;
        }
    }
}
