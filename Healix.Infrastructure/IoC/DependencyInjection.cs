using System.Text;
using Amazon;
using Amazon.Runtime;
using Amazon.S3;
using FluentValidation;
using Hangfire;
using Hangfire.PostgreSql;
using Healix.Application;
using Healix.Application.Behaviors;
using Healix.Application.Mapping;
using Healix.Application.Modules;
using Healix.Infrastructure.Configurations.EntitiesConfigurations;
using Healix.Infrastructure.Data;
using Healix.Infrastructure.Middlewares;
using Healix.Infrastructure.Services;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace Healix.Infrastructure.IoC
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddServices(
            this IServiceCollection services,
            IConfiguration configuration
        )
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));
            });

            services
                .AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        SaveSigninToken = true,

                        ValidateAudience = true,
                        ValidateIssuer = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,

                        ValidIssuer = configuration["JWT:Issuer"],
                        ValidAudience = configuration["JWT:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(configuration["JWT:SigningKey"]!)
                        ),
                    };
                });

            services.AddMediatR(cfg =>
                cfg.RegisterServicesFromAssembly(typeof(ApplicationAssemblyMarker).Assembly)
            );

            services.AddAutoMapper(cfg =>
            {
                cfg.AddMaps(typeof(MappingProfile).Assembly);
            });

            services.AddValidatorsFromAssemblyContaining<UserSignupCommand>();
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

            services.AddScoped<GlobalExceptionMiddleware>();

            services.AddScoped<IApplicationDbContext, ApplicationDbContext>();

            services.AddScoped<IJwtToken, JwtToken>();

            services.AddScoped<IEmailContext, EmailContext>();

            services.Configure<SendGridConfiguration>(configuration.GetSection("SendGrid"));
            services.AddSingleton<SendGridConfiguration>();

            services.AddScoped<WelcomeEmailStrategy>();
            services.AddScoped<OTPEmailStrategy>();

            services.AddHangfire(config =>
            {
                config
                    .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
                    .UseSimpleAssemblyNameTypeSerializer()
                    .UseRecommendedSerializerSettings()
                    .UsePostgreSqlStorage(
                        options =>
                        {
                            options.UseNpgsqlConnection(
                                configuration.GetConnectionString("DefaultConnection")
                            );
                        },
                        new PostgreSqlStorageOptions
                        {
                            QueuePollInterval = TimeSpan.FromSeconds(15),
                            JobExpirationCheckInterval = TimeSpan.FromHours(1),
                            CountersAggregateInterval = TimeSpan.FromMinutes(5),
                            PrepareSchemaIfNecessary = true, // Automatically creates Hangfire tables if they don't exist
                        }
                    );
            });

            services.Configure<AwsConfigurations>(configuration.GetSection("AWS"));

            services.AddSingleton<IAmazonS3>(sp =>
            {
                var awsConfigs = sp.GetRequiredService<IOptions<AwsConfigurations>>().Value;
                var credentials = new BasicAWSCredentials(
                    awsConfigs.AccessKey,
                    awsConfigs.SecretKey
                );
                var configs = new AmazonS3Config()
                {
                    RegionEndpoint = RegionEndpoint.GetBySystemName(awsConfigs.Region),
                };
                return new AmazonS3Client(credentials, configs);
            });

            services.AddScoped<IS3Service, S3Service>();

            services.AddHangfireServer();

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc(
                    "v1",
                    new OpenApiInfo
                    {
                        Title = "Healix",
                        Version = "v1",
                        Description = "Healix",
                    }
                );
            });

            services.Configure<GeminiConfigurations>(configuration.GetSection("Gemini"));
            services.AddSingleton<GeminiConfigurations>();

            services.AddScoped<IGeminiService, GeminiService>();

            return services;
        }
    }
}
