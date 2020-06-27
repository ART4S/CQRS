using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Serilog;
using WebFeatures.Application;
using WebFeatures.Infrastructure;
using WebFeatures.WebApi.Extensions;
using WebFeatures.WebApi.ModelBinding;

namespace WebFeatures.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;
        }

        public IConfiguration Configuration { get; }
        public IWebHostEnvironment Environment { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddApplicationServices();
            services.AddInfrastructureServices(Configuration);

            services.AddControllers(options =>
            {
                options.ModelBinderProviders.Insert(0, new FileModelBinderProvider());
            })
            .AddJsonOptions(options => options.JsonSerializerOptions.PropertyNamingPolicy = null)
            .ConfigureApiBehaviorOptions(options =>
            {
                // Disables routing by convention (only by attributes)

                options.SuppressInferBindingSourcesForParameters = false; // Uses attribute [FromBody] for complex types (IFormFile and CancellationToken - exception)
                options.SuppressConsumesConstraintForFormFileParameters = false; // Uses [FromForm] for IFormFile and IFormFileCollection
                options.SuppressModelStateInvalidFilter = true; // Automatic status 400 for unsuccessful validation
                options.SuppressMapClientErrors = true; // ProblemDetails with status 400
            });

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "WebFeatures", Version = "v1" });
            });

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie();

            services.AddMvc();
            services.AddAntiforgery(x => x.HeaderName = "X-CSRF-TOKEN");

            // For Frontend (React.JS)
            //services.AddMemoryCache();
            //services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            //services.AddReact();
            //services.AddJsEngineSwitcher(options => options.DefaultEngineName = V8JsEngine.EngineName).AddV8();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseExceptionHandling();

            app.UseHttpsRedirection();

            app.UseStaticFiles();
            app.UseDefaultFiles();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "API V1");
            });

            app.UseSerilogRequestLogging();

            app.UseCookiePolicy(new CookiePolicyOptions()
            {
                MinimumSameSitePolicy = SameSiteMode.Strict
            });

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseAntiforgery();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            //app.UseReact(config => { });
        }
    }
}