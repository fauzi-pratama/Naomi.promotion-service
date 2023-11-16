
using Microsoft.EntityFrameworkCore;
using Naomi.promotion_service.Models.Entities;

namespace Naomi.promotion_service.Models.Contexts
{
    public class DataDbContext : DbContext
    {
        public DataDbContext(DbContextOptions<DataDbContext> options) : base(options)
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true); //for set timestamp without timezone     
        }

        public DbSet<PromoWorkflow> PromoWorkflow { get; set; }
        public DbSet<PromoWorkflowExpression> PromoWorkflowExpression { get; set; }
        public DbSet<PromoRule> PromoRule { get; set; }
        public DbSet<PromoRuleMop> PromoRuleMop { get; set; }
        public DbSet<PromoRuleVariable> PromoRuleVariable { get; set; }
        public DbSet<PromoRuleExpression> PromoRuleExpression { get; set; }
        public DbSet<PromoRuleResult> PromoRuleResult { get; set; }
        public DbSet<PromoTrans> PromoTrans { get; set; }
        public DbSet<PromoTransDetail> PromoTransDetail { get; set; }
        public DbSet<PromoMaster> PromoMaster { get; set; }
        public DbSet<PromoMasterMop> PromoMasterMop { get; set; }
        public DbSet<PromoMasterClass> PromoMasterClass { get; set; }
        public DbSet<PromoMasterType> PromoMasterType { get; set; }
        public DbSet<PromoMasterZone> PromoMasterZone { get; set; }
        public DbSet<PromoOtp> PromoOtp { get; set; }
        public DbSet<PromoMasterUserEmail> PromoMasterUserEmail { get; set; }
        public DbSet<PromoRuleApps> PromoRuleApps { get; set; }
        public DbSet<PromoRuleMembership> PromoRuleMembership { get; set; }
        public DbSet<PromoMasterSite> PromoMasterSite { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PromoMasterClass>().HasData(
                new PromoMasterClass()
                {
                    Id = new Guid("302BE9CD-5E08-454D-B8E5-582D336750D7"),
                    PromotionClassKey = "ITEM",
                    PromotionClassName = "ITEM",
                    LineNum = 1,
                    ActiveFlag = true,
                    CreatedBy = "System",
                    CreatedDate = DateTime.Now,
                    UpdatedBy = "System",
                    UpdatedDate = DateTime.Now
                },
                new PromoMasterClass()
                {
                    Id = new Guid("8713BD36-48D6-43DD-94B9-407C3AFF1528"),
                    PromotionClassKey = "CART",
                    PromotionClassName = "CART",
                    LineNum = 2,
                    ActiveFlag = true,
                    CreatedBy = "System",
                    CreatedDate = DateTime.Now,
                    UpdatedBy = "System",
                    UpdatedDate = DateTime.Now
                },
                new PromoMasterClass()
                {
                    Id = new Guid("DBF358CB-F43B-4D69-9176-8EE63AC8953F"),
                    PromotionClassKey = "MOP",
                    PromotionClassName = "MOP",
                    LineNum = 3,
                    ActiveFlag = true,
                    CreatedBy = "System",
                    CreatedDate = DateTime.Now,
                    UpdatedBy = "System",
                    UpdatedDate = DateTime.Now
                },
                new PromoMasterClass()
                {
                    Id = new Guid("C386C5F1-D3D2-4E7F-AD6A-34B4F185325C"),
                    PromotionClassKey = "Entertain",
                    PromotionClassName = "Entertain",
                    LineNum = 4,
                    ActiveFlag = true,
                    CreatedBy = "System",
                    CreatedDate = DateTime.Now,
                    UpdatedBy = "System",
                    UpdatedDate = DateTime.Now
                }
            );

            modelBuilder.Entity<PromoMasterType>().HasData(
                new PromoMasterType()
                {
                    Id = new Guid("FAC8E236-2FB7-4B4A-B644-0680F60FD0A0"),
                    PromotionClassId = "302BE9CD-5E08-454D-B8E5-582D336750D7",
                    PromotionTypeKey = "ITEM",
                    PromotionTypeName = "BUY X GET Y ITEM",
                    LineNum = 1,
                    ActiveFlag = true,
                    CreatedBy = "System",
                    CreatedDate = DateTime.Now,
                    UpdatedBy = "System",
                    UpdatedDate = DateTime.Now
                },
                new PromoMasterType()
                {
                    Id = new Guid("E0D70F81-6A25-434D-9055-E50554EF585C"),
                    PromotionClassId = "302BE9CD-5E08-454D-B8E5-582D336750D7",
                    PromotionTypeKey = "SP",
                    PromotionTypeName = "SPECIAL PRICE ITEM",
                    LineNum = 1,
                    ActiveFlag = true,
                    CreatedBy = "System",
                    CreatedDate = DateTime.Now,
                    UpdatedBy = "System",
                    UpdatedDate = DateTime.Now
                },
                new PromoMasterType()
                {
                    Id = new Guid("1F57489B-CCA0-4392-AE00-3D145012D375"),
                    PromotionClassId = "302BE9CD-5E08-454D-B8E5-582D336750D7",
                    PromotionTypeKey = "AMOUNT",
                    PromotionTypeName = "DISCOUNT AMOUNT ITEM",
                    LineNum = 2,
                    ActiveFlag = true,
                    CreatedBy = "System",
                    CreatedDate = DateTime.Now,
                    UpdatedBy = "System",
                    UpdatedDate = DateTime.Now
                },
                new PromoMasterType()
                {
                    Id = new Guid("886470D3-5E0B-41ED-BAA7-10CD94511E10"),
                    PromotionClassId = "302BE9CD-5E08-454D-B8E5-582D336750D7",
                    PromotionTypeKey = "PERCENT",
                    PromotionTypeName = "DISCOUNT PERCENT ITEM",
                    LineNum = 2,
                    ActiveFlag = true,
                    CreatedBy = "System",
                    CreatedDate = DateTime.Now,
                    UpdatedBy = "System",
                    UpdatedDate = DateTime.Now
                },
                new PromoMasterType()
                {
                    Id = new Guid("BD4F0C46-7D03-45FA-B33C-77028218593A"),
                    PromotionClassId = "302BE9CD-5E08-454D-B8E5-582D336750D7",
                    PromotionTypeKey = "BUNDLE",
                    PromotionTypeName = "DISCOUNT BUNDLING ITEM",
                    LineNum = 2,
                    ActiveFlag = true,
                    CreatedBy = "System",
                    CreatedDate = DateTime.Now,
                    UpdatedBy = "System",
                    UpdatedDate = DateTime.Now
                },
                new PromoMasterType()
                {
                    Id = new Guid("86ED449A-E4BC-4C28-A6E5-3BA18E491E63"),
                    PromotionClassId = "8713BD36-48D6-43DD-94B9-407C3AFF1528",
                    PromotionTypeKey = "AMOUNT",
                    PromotionTypeName = "DISCOUNT AMOUNT CART",
                    LineNum = 2,
                    ActiveFlag = true,
                    CreatedBy = "System",
                    CreatedDate = DateTime.Now,
                    UpdatedBy = "System",
                    UpdatedDate = DateTime.Now
                },
                new PromoMasterType()
                {
                    Id = new Guid("2524251A-565A-46C0-93D5-DEEA80C63FF5"),
                    PromotionClassId = "8713BD36-48D6-43DD-94B9-407C3AFF1528",
                    PromotionTypeKey = "PERCENT",
                    PromotionTypeName = "DISCOUNT PERCENT CART",
                    LineNum = 2,
                    ActiveFlag = true,
                    CreatedBy = "System",
                    CreatedDate = DateTime.Now,
                    UpdatedBy = "System",
                    UpdatedDate = DateTime.Now
                },
                new PromoMasterType()
                {
                    Id = new Guid("3C7ED57D-8235-453F-8F97-BA93B3747B4F"),
                    PromotionClassId = "DBF358CB-F43B-4D69-9176-8EE63AC8953F",
                    PromotionTypeKey = "AMOUNT",
                    PromotionTypeName = "DISCOUNT AMOUNT MOP",
                    LineNum = 2,
                    ActiveFlag = true,
                    CreatedBy = "System",
                    CreatedDate = DateTime.Now,
                    UpdatedBy = "System",
                    UpdatedDate = DateTime.Now
                },
                new PromoMasterType()
                {
                    Id = new Guid("DDA43968-95BD-4D94-8737-FD621D0A5895"),
                    PromotionClassId = "DBF358CB-F43B-4D69-9176-8EE63AC8953F",
                    PromotionTypeKey = "PERCENT",
                    PromotionTypeName = "DISCOUNT PERCENT MOP",
                    LineNum = 2,
                    ActiveFlag = true,
                    CreatedBy = "System",
                    CreatedDate = DateTime.Now,
                    UpdatedBy = "System",
                    UpdatedDate = DateTime.Now
                },
                new PromoMasterType()
                {
                    Id = new Guid("57AE0D50-1D3B-4A33-8D7C-A4CAB863AA30"),
                    PromotionClassId = "C386C5F1-D3D2-4E7F-AD6A-34B4F185325C",
                    PromotionTypeKey = "PERCENT",
                    PromotionTypeName = "DISCOUNT PERCENT Entertain",
                    LineNum = 2,
                    ActiveFlag = true,
                    CreatedBy = "System",
                    CreatedDate = DateTime.Now,
                    UpdatedBy = "System",
                    UpdatedDate = DateTime.Now
                }
            );
        }
    }
}
