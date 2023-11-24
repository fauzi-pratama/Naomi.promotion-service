
using System.Reflection;
using System.Security.Cryptography;
using FluentValidation.AspNetCore;
using Swashbuckle.AspNetCore.Filters;
using VaultSharp.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Naomi.promotion_service.Configurations;
using Naomi.promotion_service.Models.Contexts;
using Naomi.promotion_service.Services.SAPService;
using Naomi.promotion_service.Services.EmailService;
using Naomi.promotion_service.Services.EngineService;
using Naomi.promotion_service.Services.CommitService;
using Naomi.promotion_service.Services.OtpPromoService;
using Naomi.promotion_service.Services.FindPromoService;
using Naomi.promotion_service.Services.PromoSetupService;
using Naomi.promotion_service.Services.SoftBookingService;
using Naomi.promotion_service.Services.WorkflowPromoService;

//Config App
var builder = WebApplication.CreateBuilder(args);

//Config Vault
VaultConfig? vaultConfig = builder.Configuration.GetSection("Vault").Get<VaultConfig>();
builder.Configuration.AddJsonFile("appsettings.json").AddVaultConfiguration(() =>
    new VaultOptions(vaultAddress: vaultConfig.Link!, vaultToken: vaultConfig.Token), vaultConfig.BasePath!, vaultConfig.MountPoint!);

//Config Env
builder.Services.Configure<AppConfig>(builder.Configuration);
AppConfig? appConfig = builder.Configuration.Get<AppConfig>();

//Config DB Postgres
builder.Services.AddDbContext<DataDbContext>(options => {
    options.UseNpgsql(appConfig.PostgreSqlConnectionString!);
});

//Config Cap Kafka
builder.Services.AddCap(x =>
{
    x.UsePostgreSql(opt =>
    {
        opt.Schema = "cap";
        opt.ConnectionString = appConfig.PostgreSqlConnectionString!;
    });

    x.UseKafka(opt =>
    {
        opt.Servers = appConfig.KafkaConnectionString!;
        opt.CustomHeaders = kafkaResult => new List<KeyValuePair<string, string>>
        {
            new ("cap-msg-id", Guid.NewGuid().ToString()),
            new ("cap-msg-name", kafkaResult.Topic)
        };
    });

    x.UseDashboard();
});

//Dependency Injection
builder.Services.AddScoped<IOtpService, OtpService>();
builder.Services.AddScoped<ISAPService, SAPService>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<IEngineService, EngineService>();
builder.Services.AddScoped<ICommitService, CommitService>();
builder.Services.AddScoped<IWorkflowService, WorkflowService>();
builder.Services.AddScoped<IFindPromoService, FindPromoService>();
builder.Services.AddScoped<ISoftBookingService, SoftBookingService>();
builder.Services.AddSingleton<IPromoSetupService, PromoSetupService>();

//Background Service Setup Promo Workflow
builder.Services.AddHostedService<InitialPromoBackground>();

//Config Automapper
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

//Config Jwt Auth
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    var rsa = RSA.Create();
    rsa.ImportFromPem(appConfig.PublicKey!);

    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateAudience = false,
        ValidateIssuer = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new RsaSecurityKey(rsa)
    };
});

//Confgi Filter Validation
builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});

//Config Controller
builder.Services.AddControllers(option =>
{
    option.Filters.Add(typeof(ValidateModelResponse));
}).AddFluentValidation(v =>
{
    v.ImplicitlyValidateChildProperties = true;
    v.ImplicitlyValidateRootCollectionElements = true;
    v.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly());
});

//Config Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => {
    c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Description = "Standart Authorize Bearer",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });

    c.OperationFilter<SecurityRequirementsOperationFilter>();
});

var app = builder.Build();

//Apply MigrateDb
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<DataDbContext>();
    db.Database.Migrate();
}

//Run Swagger
app.UseSwagger();
app.UseSwaggerUI(c => {
    c.DefaultModelsExpandDepth(-1);
});

//Run Https
app.UseHttpsRedirection();

//Run Auth
app.UseAuthentication();
app.UseAuthorization();

//Run Contoller
app.MapControllers();

//Run App
app.Run();