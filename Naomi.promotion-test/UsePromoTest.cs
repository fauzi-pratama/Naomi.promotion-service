
using Moq;
using AutoMapper;
using Newtonsoft.Json;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using Naomi.promotion_service.Models.Dto;
using Naomi.promotion_service.Models.Testing;
using Naomi.promotion_service.Models.Request;
using Naomi.promotion_service.Models.Response;
using Naomi.promotion_service.Models.Contexts;
using Naomi.promotion_service.Configurations;
using Naomi.promotion_service.Services.OtpPromoService;
using Naomi.promotion_service.Services.FindPromoService;
using Naomi.promotion_service.Services.PromoSetupService;
using Naomi.promotion_service.Services.SoftBookingService;
using Naomi.promotion_service.Services.WorkflowPromoService;
using Naomi.promotion_service.Services.EmailService;

namespace Naomi.promotion_test
{
    public class UsePromoTest
    {
        private readonly IMapper _mapper;
        private readonly PromoSetupService _promoSetupService;
        private static DataDbContext? _dbContext;

        public UsePromoTest()
        {
            _mapper = GetIMapper();
            _dbContext = GetDbContext();
            _promoSetupService = GetPromoSetupService();
        }

        private static DataDbContext GetDbContext()
        {
            DbContextOptions<DataDbContext> dbContextOptions = new DbContextOptionsBuilder<DataDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            DataDbContext dataDbContext = new(dbContextOptions);

            DbTesting dbTesting = new();

            dataDbContext.PromoWorkflow.AddRange(dbTesting.GetListPromoWorkFlow());
            dataDbContext.PromoMasterZone.AddRange(dbTesting.GetListPromoMasterZone());
            dataDbContext.PromoMasterSite.AddRange(dbTesting.GetListPromoMasterSite());
            dataDbContext.SaveChanges();

            return dataDbContext;
        }

        private static IOptions<AppConfig> GetAppConfig()
        {
            AppConfig appConfig = new()
            {
                PostgreSqlConnectionString = "-",
                KafkaConnectionString = "-",
                EmailDomain = "-",
                EmailHost = "-",
                EmailPort = 123
            };

            return Options.Create(appConfig);
        }

        private static PromoSetupService GetPromoSetupService()
        {
            WorkflowService workflowService = new(_dbContext!);
            PromoSetupService promoSetupService = new();

            string[] listWorkflow = workflowService.GetWorkflow();
            promoSetupService.RefreshWorkflow(listWorkflow);

            return promoSetupService;
        }

        private static Mock<IEmailService> GetMockEmailService()
        {
            IEmailService emailService = new EmailService(_dbContext!, GetAppConfig());
            Mock<IEmailService> mockEmailService = new();

            return mockEmailService;
        }

        private static Mock<IOtpService> GetMockOtpService()
        {
            OtpService otpService = new(_dbContext!, GetAppConfig(), GetMockEmailService().Object);
            Mock<IOtpService> mockOtpService = new();

            return mockOtpService;
        }

        private static async Task<Mock<ISoftBookingService>> GetMockSoftBookingService(List<PromoRuleCekAvailRequest> promoRuleCekAvailRequests, string companyCode)
        {
            SoftBookingService softBookingService = new(_dbContext!);
            Mock<ISoftBookingService> mockSoftBookingService = new();

            (List<string> data, string message, bool cek) = await softBookingService.CekPromoAvail(promoRuleCekAvailRequests!, companyCode!);

            mockSoftBookingService.Setup(q => q.CekPromoAvail(It.IsAny<List<PromoRuleCekAvailRequest>>(), It.IsAny<string>())).ReturnsAsync((data, message, cek));

            return mockSoftBookingService;
        }

        private static IMapper GetIMapper()
        {
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutoMapperProfile());
            });

            return mockMapper.CreateMapper();
        }

        private async Task<(List<FindPromoResponse>, string, bool)> FindPromoTest(FindPromoRequest findPromoRequest, List<PromoRuleCekAvailRequest> promoRuleCekAvailRequests)
        {
            FindPromoService findPromoService =
                new(_dbContext!, _promoSetupService, GetMockOtpService().Object,
                    GetMockSoftBookingService(promoRuleCekAvailRequests, findPromoRequest.CompanyCode!).Result.Object, _mapper);

            return await findPromoService.FindPromo(findPromoRequest);
        }

        [Fact]
        public async void TestPromoSetupService()
        {
            //Request
            FindPromoRequest findPromoRequest = new()
            {
                TransId = "001",
                TransDate = "2023-02-01 10:00:00",
                CompanyCode = "F100",
                SiteCode = "F314",
                ZoneCode = "Z1",
                EntertaimentStatus = false,
                PromoAppCode = "ODI",
                Member = false,
                NewMember = false,
                ItemProduct = new()
                {
                    new()
                    {
                        LineNo = 1,
                        SkuCode = "milktea001",
                        DeptCode = "CHT",
                        Qty = 2,
                        Price = 25000,
                    },
                    new()
                    {
                        LineNo = 2,
                        SkuCode = "milktea002",
                        DeptCode = "CHT",
                        Qty = 1,
                        Price = 15000,
                    }
                }
            };

            //ResponseExpected
            List<FindPromoResponse> findPromoResponses = new()
            {
                new ()
                {
                    TransId = "001",
                    CompanyCode = "F100",
                    PromoCode = "PROMO001",
                    PromoName = "PROMO001_CLASS(CART)_TYPE(AMOUNT)_REQ(AND)_SBOOK(FALSE)_MULTIPLE(FALSE)_RESULT(AND)",
                    PromoType = "AMOUNT",
                    PromoTypeResult = "ALL",
                    ValDiscount = "10000",
                    ValMaxDiscount = "",
                    ValMaxDiscountStatus = false,
                    PromoCls = 2,
                    PromoLvl = 2,
                    MaxMultiple = 1,
                    MaxUse = 0,
                    MaxBalance = 0,
                    MultipleQty = 1,
                    PromoDesc = "Desction Promo Cart Amount",
                    PromoTermCondition = "Terms Condition Promo Cart Amount",
                    PromoImageLink = "",
                    PromoMopRequire = null,
                    PromoListItem = new List<PromoListItem>()
                    {
                        new()
                        {
                            LinePromo = 1,
                            TotalBefore = 65000,
                            TotalDiscount = 10000,
                            TotalAfter = 55000,
                            Rounding = 1,
                            PromoListItemDetail = new List<PromoListItemDetail>()
                            {
                                new()
                                {
                                    LineNo = 1,
                                    SkuCode = "milktea001",
                                    ValDiscount = "10000",
                                    ValMaxDiscount = "",
                                    Price = 25000,
                                    Qty = 2,
                                    TotalPrice = 50000,
                                    TotalDiscount = 7692,
                                    TotalAfter = 42308
                                },
                                new ()
                                {
                                    LineNo = 2,
                                    SkuCode = "milktea002",
                                    ValDiscount = "10000",
                                    ValMaxDiscount = "",
                                    Price = 15000,
                                    Qty = 1,
                                    TotalPrice = 15000,
                                    TotalDiscount = 2307,
                                    TotalAfter = 12693
                                }
                            }
                        }
                    }
                }
            };

            //Model CekAvail for SoftBooking
            List<PromoRuleCekAvailRequest> promoRuleCekAvailRequests = new()
            {
                new()
                {
                    PromoCode = "PROMO001",
                    BalanceUse = 10000,
                    QtyUse = 1,
                    MaxBalance = 0,
                    MaxUse = 0,
                    CekRefCode = false,
                    RefCode = null
                }
            };

            //Run Function Testing
            (List<FindPromoResponse> data, string message, bool cek) = await FindPromoTest(findPromoRequest, promoRuleCekAvailRequests);

            //Convert DataResponse to String
            string dataResponseExpString = JsonConvert.SerializeObject(findPromoResponses);
            string dataResponseActString = JsonConvert.SerializeObject(data);

            //Testing
            Assert.True(cek);
            Assert.Equal("SUCCESS", message);
            Assert.Equal(dataResponseExpString, dataResponseActString);
        }
    }
}
