
using Naomi.promotion_service.Services.PromoSetupService;
using Naomi.promotion_service.Services.WorkflowPromoService;

namespace Naomi.promotion_service.Configurations
{
    public class InitialPromoBackground : BackgroundService
    {
        private readonly IServiceScopeFactory _factory;
        private readonly ILogger<InitialPromoBackground> _logger;

        public InitialPromoBackground(IServiceScopeFactory factory, ILogger<InitialPromoBackground> logger)
        {
            _factory = factory;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await using AsyncServiceScope asyncScope = _factory.CreateAsyncScope();

            IWorkflowService setupPromoWorkflow = asyncScope.ServiceProvider.GetRequiredService<IWorkflowService>();
            IPromoSetupService setupPromoService = asyncScope.ServiceProvider.GetRequiredService<IPromoSetupService>();

            string[] dataPromoWorkflow = await setupPromoWorkflow.GetWorkflow();
            (bool cekSetupPromo, string messageSetupPromo ) = setupPromoService.RefreshWorkflow(dataPromoWorkflow);

            if (cekSetupPromo)
                _logger.LogInformation("Success Setup Promo");
            else
                _logger.LogError($"Failed Setup Promo : {messageSetupPromo}");
        }
    }
}
