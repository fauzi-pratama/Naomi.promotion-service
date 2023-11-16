
//Config App
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Naomi.promotion_service.Configurations;
using Naomi.promotion_service.Models.Contexts;
using Naomi.promotion_service.Services.SAPService;
using Swashbuckle.AspNetCore.Filters;

var builder = WebApplication.CreateBuilder(args);

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
            new KeyValuePair<string, string>("cap-msg-id", Guid.NewGuid().ToString()),
            new KeyValuePair<string, string>("cap-msg-name", kafkaResult.Topic)
        };
    });

    x.UseDashboard();
});

//Dependency Injection
builder.Services.AddScoped<ISAPService, SAPService>();

//Config Controller
builder.Services.AddControllers();

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

//Run Swagger
app.UseSwagger();
app.UseSwaggerUI(c => {
    c.DefaultModelsExpandDepth(-1);
});

//Run Https
app.UseHttpsRedirection();

//Run Auth
app.UseAuthorization();

//Run Contoller
app.MapControllers();

//Run App
app.Run();
