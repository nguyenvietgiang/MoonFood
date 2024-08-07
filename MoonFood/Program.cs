﻿using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MoonBussiness.CommonBussiness;
using MoonBussiness.CommonBussiness.Auth;
using MoonBussiness.CommonBussiness.Dapper;
using MoonBussiness.CommonBussiness.File;
using MoonBussiness.GraphQL;
using MoonBussiness.Interface;
using MoonBussiness.Repository;
using MoonBussiness.Validator;
using MoonDataAccess;
using MoonModels.DTO.RequestDTO;
using Serilog.Events;
using Serilog;
using Syncfusion.Licensing;
using System.Reflection;
using System.Text;
using Serilog.Formatting.Json;
using Google.Api;
using MoonFood.Middlewares;
using MoonFood.Common.RedisCache;
using Hangfire;
using Hangfire.MemoryStorage;
using MoonFood.Common.CommonModels;
using Microsoft.AspNetCore.Mvc.Versioning;

var builder = WebApplication.CreateBuilder(args);
//ghi log vào file
//Log.Logger = new LoggerConfiguration()
//    .MinimumLevel.Debug()
//    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning) 
//    .Enrich.FromLogContext()
//    .WriteTo.File(new JsonFormatter(), "logs/log.txt", rollingInterval: RollingInterval.Day)
//    .CreateLogger();
// Add services to the container.
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
var connectString = builder.Configuration.GetConnectionString("ApiCodeFirst");
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Logging.AddSerilog();
builder.Services.AddLogging();
builder.Services.AddControllers()
        .AddFluentValidation(fv => fv.ImplicitlyValidateChildProperties = true);
builder.Services.AddControllers().AddNewtonsoftJson();
//cache
builder.Services.AddMemoryCache();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Restful API for Moon Food Store.",
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] { }
        }
    });
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});

builder.Services.AddDbContext<DataContext>(options =>
options.UseNpgsql(connectString));
builder.Services.AddAutoMapper(typeof(MappingProfile));

SyncfusionLicenseProvider.RegisterLicense("MTQwNUAzMTM4MmUzNDJlMzBGT29sdENza2kyME1jUHpPNVd5enVXY1AvNVZ1SVdPQlVMNUE4R1c1M0FvPQ==");

builder.Services.AddScoped<IDataAcess, DataAccess>();

builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IFileService, FileService>();

// GraphQL
builder.Services.AddScoped<Query>();
builder.Services.AddScoped<Mutation>();
builder.Services.AddGraphQLServer()
    .AddQueryType<Query>()
    .AddMutationType<Mutation>();

//redis cache
builder.Services.AddScoped(typeof(ICacheService<>), typeof(CacheService<>));

builder.Services.AddScoped<MiddlewareExceptionHandling>();
builder.Services.AddScoped<MiddlewareCaching>();

builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddScoped<IEmailRepository, EmailRepository>();
builder.Services.AddScoped<ITableRepository, TableRepository>();
builder.Services.AddScoped<IBookingRepository, BookingRepository>();
builder.Services.AddScoped<IStatisticalRepositpry, StatisticalRepository>();
builder.Services.AddScoped<IExelRepository, ExelRepository>();
builder.Services.AddScoped<IFoodRepositorycs, FoodRepository>();
builder.Services.AddScoped<IComboRepository, ComboRepository>();
builder.Services.AddScoped<IOderRepository, OderRepository>();

builder.Services.AddTransient<IValidator<CreateAccountRequest>, CreateAccountRequestValidator>();
builder.Services.AddTransient<IValidator<LoginRequest>, LoginRequestValidator>();
builder.Services.AddTransient<IValidator<ChangePassword>, ChangePasswordValidator>();
builder.Services.AddTransient<IValidator<CreateFoodRequest>, CreateFoodRequestValidator>();
builder.Services.AddTransient<IValidator<ComboRequest>, ComboRequestValidator>();

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                   policy =>
                   {
                       policy.AllowAnyOrigin()
                             .AllowAnyMethod()
                             .AllowAnyHeader();
                   });
});
builder.Services.AddHangfire(configuration => configuration.UseMemoryStorage());
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = "moonfood2023",
            ValidAudience = "my-audience",
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("this is my custom Secret key for authentication"))
        };
    });

builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetConnectionString("Redis");
});


builder.Services.AddHealthChecks().AddNpgSql(connectString);
builder.Services.AddHealthChecksUI().AddInMemoryStorage();
// push notification
builder.Services.Configure<OneSignalConfig>(builder.Configuration.GetSection("OneSignalConfig"));
builder.Services.AddHttpClient();

// api versioning
builder.Services.AddApiVersioning(options =>
{
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.DefaultApiVersion = new Microsoft.AspNetCore.Mvc.ApiVersion(1, 0);
    options.ReportApiVersions = true;
    options.ApiVersionReader = ApiVersionReader.Combine(
    // new QueryStringApiVersionReader("api-version"),
     new HeaderApiVersionReader("x-version")//,
                                            //new MediaTypeApiVersionReader("ver")
    );
});

builder.Services.AddVersionedApiExplorer(options =>
{
    options.GroupNameFormat = "'v'VVV";
    options.SubstituteApiVersionInUrl = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseStaticFiles();

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
            Path.Combine(Directory.GetCurrentDirectory(), "file-uploads")),
    RequestPath = "/file-uploads"
});


app.UseCors(MyAllowSpecificOrigins);
// áp dụng api key ở đây nếu muốn dùng
//app.UseMiddleware<ApiKeyMiddleware>("11102001201219722562005");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseMiddleware<MiddlewareExceptionHandling>();
//app.UseMiddleware<MiddlewareCaching>();
app.UseHangfireServer();
app.UseHangfireDashboard("/hangfile");
app.MapGraphQL("/graphql");
app.MapHealthChecks("/healthcheck");
app.MapHealthChecksUI();

app.Run();
