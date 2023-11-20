
using Moq;
using Microsoft.EntityFrameworkCore;
using Naomi.promotion_service.Models.Contexts;
using Naomi.promotion_service.Models.Testing;
using Naomi.promotion_service.Services.PromoSetupService;
using Naomi.promotion_service.Services.WorkflowPromoService;
using Naomi.promotion_service.Models.Response;
using Naomi.promotion_service.Models.Request;
using Naomi.promotion_service.Services.FindPromoService;
using Naomi.promotion_service.Services.SoftBookingService;
using Naomi.promotion_service.Services.OtpPromoService;

namespace Naomi.promotion_test
{
    public class UsePromoTest
    {
        private readonly PromoSetupService _promoSetupService;
        private static DataDbContext? _dbContext;

        public UsePromoTest()
        {
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

        private static PromoSetupService GetPromoSetupService()
        {
            WorkflowService workflowService = new(_dbContext!);
            PromoSetupService promoSetupService = new();

            string[] listWorkflow = workflowService.GetWorkflow();
            promoSetupService.RefreshWorkflow(listWorkflow);

            return promoSetupService;
        }

        private async Task<Mock<IOtpService>> GetMockOtpService()
        {
            OtpService otpService = new(_dbContext!);
            Mock<IOtpService> mockOtpService = new();

            return mockOtpService;
        }

        private async Task<Mock<ISoftBookingService>> GetMockSoftBookingService()
        {
            SoftBookingService softBookingService = new(_dbContext!);
            Mock<ISoftBookingService> mockSoftBookingService = new();

            return mockSoftBookingService;
        }

        //private async Task<(List<FindPromoResponse>, string, bool)> FindPromoTest(FindPromoRequest findPromoRequest)
        //{
        //    FindPromoService findPromoService = new(_dbContext!, _promoSetupService, GetMockOtpService().Result.Object ,GetMockSoftBookingService().Result.Object, );
        //}

        [Fact]
        public void TestPromoSetupService()
        {
            Assert.Equal("test","test");
        }
    }
}
