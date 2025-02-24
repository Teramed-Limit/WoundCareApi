using System.Text;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using WoundCareApi.AutoMapper;
using WoundCareApi.Persistence.UnitOfWork;
using WoundCareApi.Persistence.Repository;
using WoundCareApi.src.Infrastructure.Persistence;
using WoundCareApi.API.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// 設定 Serilog 作為日誌處理器
Log.Logger = new LoggerConfiguration().ReadFrom.Configuration(builder.Configuration).CreateLogger();

// 使用 Serilog 作為日誌處理器
builder.Host.UseSerilog();

// 獲取應用配置
var configuration = builder.Configuration;

// 配置 Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc(
        "v1",
        new OpenApiInfo
        {
            Title = "Your API",
            Version = "v1",
            Description = "Your API Description"
        }
    );

    // 添加 JWT 認證配置
    c.AddSecurityDefinition(
        "Bearer",
        new OpenApiSecurityScheme
        {
            Description =
                "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
            Name = "Authorization",
            In = ParameterLocation.Header,
            Type = SecuritySchemeType.ApiKey,
            Scheme = "Bearer"
        }
    );

    c.AddSecurityRequirement(
        new OpenApiSecurityRequirement
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
                Array.Empty<string>()
            }
        }
    );
});

// 配置 JWT 認證
builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = configuration["Jwt:Issuer"],
            ValidAudience = configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(configuration["Jwt:Key"])
            )
        };
    });

// 設定靜態文件服務
builder.Services.AddSpaStaticFiles(configuration =>
{
    configuration.RootPath = "ClientApp/build";
});

// 配置 CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy(
        "AllowAll",
        builder =>
        {
            builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
        }
    );
});

// 配置多個 DbContext
builder.Services.AddDbContext<CRSDbContext>(options =>
{
    options
        .UseSqlServer(
            builder.Configuration.GetConnectionString("CRSConnection"),
            b => b.MigrationsAssembly("WoundCareApi.src.Infrastructure.Persistence")
        )
        .EnableSensitiveDataLogging();
    if (Convert.ToBoolean(configuration["SQLDebug"]))
        options.LogTo(Console.WriteLine);
});

// 設定 AutoMapper
builder.Services.AddSingleton(
    provider =>
        new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new AutoMapperProfiles());
            // cfg.AddProfile(new AutoMapperProfiles(provider.GetService<ConfigService>()));
        }).CreateMapper()
);

// 註冊 UnitOfWork
builder.Services.AddScoped<IUnitOfWork, GenericUnitOfWork<CRSDbContext>>();

// 註冊 Repositories
builder.Services.AddScoped(typeof(IRepository<,>), typeof(GenericRepository<,>));

// 註冊 Services
builder.Services.AddScoped<CaseService>();
builder.Services.AddScoped<ShiftTimeService>();
builder.Services.AddScoped<UnitShiftService>();
builder.Services.AddScoped<ICaseReportService, CaseReportService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "WoundCareApi V1");
        c.OAuthClientId("swagger-ui");
        c.OAuthAppName("Swagger UI for WoundCareApi");
    });
}

// 配置應用以服務靜態文件和 SPA
app.UseDefaultFiles();
app.UseSpaStaticFiles();

// app.UseRouting();

// 添加認證中間件
app.UseHttpsRedirection();

// 使用 CORS
app.UseCors("AllowAll");

// 添加 Serilog 請求記錄
if (app.Environment.IsDevelopment())
    app.UseSerilogRequestLogging();

// 添加認證中間件
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.UseSpa(spa =>
{
    spa.Options.SourcePath = "ClientApp";
});

app.Run();
