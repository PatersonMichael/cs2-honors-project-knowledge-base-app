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
using SharpGrip.FluentValidation.AutoValidation.Mvc.Extensions;

namespace KB.Web.API
{
    public class Program
    {
        public static IConfigurationBuilder Configuration { get; } = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile(
                $"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json",
                optional: true)
            .AddEnvironmentVariables();

        public static int Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


           

            // Configure Services

            // Auto mapper
            builder.Services.AddAutoMapper(typeof(Program).Assembly);

            // db context pool
            builder.Services.AddDbContextPool<KnowledgeBaseAppContext>(opts =>
            {
                opts.UseSqlServer(builder.Configuration.GetConnectionString("KnowledgeBaseAppConnection"));
            });

            builder.Services.AddRepsitories();

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

            var app = builder.Build();

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

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            // Setup logger
            // using Serilog library for nice logging
            Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .MinimumLevel.Debug()
                .WriteTo.Console(
                    outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj} {Properties:j}{NewLine}{Exception}")
                .CreateLogger();

            //TODO add logging to files OR database?
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