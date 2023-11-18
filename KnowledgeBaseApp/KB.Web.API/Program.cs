using System.Text.Json;
using System.Text.Json.Serialization;
using FluentValidation;
using GT.Web.Api.Filters;
using KB.Domain;
using KB.Web.API.Configuration;
using KB.Web.API.Validators;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Events;
using Serilog.Formatting.Compact;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Extensions;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text;
using KB.Web.API.Middleware;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace KB.Web.API
{
    public class Program
    {

        public static int Main(string[] args)
        {

            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", true)
                .Build();

            // Setup logger
            // using Serilog library for nice logging

            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .CreateLogger();

            //TODO add logging to files OR database?

            var builder = WebApplication.CreateBuilder(args);

            // Configure Services

            // Auto mapper
            builder.Services.AddAutoMapper(typeof(Program).Assembly);

            // db context pool
            builder.Services.AddDbContextPool<KnowledgeBaseAppContext>(opts =>
            {
                opts.UseSqlServer(builder.Configuration.GetConnectionString("KnowledgeBaseAppConnection"));
            });


            builder.Services.AddRepositories();
            //builder.Services.AddTokenPayloadContext();

            builder.Services.AddControllers(opts =>
                {
                    // Add filter to catch all types of exceptions
                    opts.Filters.Add(typeof(GlobalExceptionFilter));
                })
                .AddJsonOptions(opts =>
                {
                    // send json as camelcase
                    opts.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter(JsonNamingPolicy.CamelCase));
                    // indent json objects
                    opts.JsonSerializerOptions.WriteIndented = true;
                    // in case properties come in different case
                    opts.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
                });

            // FluentValidation
            builder.Services.AddFluentValidationAutoValidation();
            builder.Services.AddValidatorsFromAssemblyContaining<UserProfileValidator>();

            // Authentication
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(opts => 
                    opts.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidIssuer = configuration["JwtSettings:Issuer"],
                        ValidAudience = configuration["JwtSettings:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey
                            (Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("JWT_SECRET"))),
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateIssuerSigningKey = true,
                        ValidateLifetime = true
                    }
                    );

            builder.Host.UseSerilog();

            // Swagger
            builder.Services.AddSwaggerGen(opts =>
            {
                opts.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Knowledge Base API",
                    Description = "An ASP.NET Core Web API for managing Knowledge Base App Entities",
                });
            });

            builder.Services.AddTokenPayloadContext();

            var app = builder.Build();


            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(options =>
                {
                    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
                    options.RoutePrefix = string.Empty;
                });
            }

            app.UseSerilogRequestLogging();

            // Cross origin resource sharing
            // Enables access and sharing between different entities on internet
            app.UseCors(opts =>
            {
                opts.AllowAnyHeader();
                opts.AllowAnyMethod();
                opts.AllowAnyOrigin();
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();
            app.UseTokenPayloadContext();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });


           
            try
            {

                Log.Information("Starting Web API");

                app.MapGet("/", () => "Hello World!");

                app.Run();

                return 0;

            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Host terminated unexpectedly");
                return 1;
            }
            finally
            {
                Log.CloseAndFlush();
            }
            




        }
    }
}