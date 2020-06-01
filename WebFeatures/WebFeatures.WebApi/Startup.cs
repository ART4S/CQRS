using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using WebFeatures.Application;
using WebFeatures.Infrastructure;
using WebFeatures.WebApi.Middlewares;
using WebFeatures.WebApi.ModelBinders;

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

            services.AddControllers(opt =>
            {
                opt.ModelBinderProviders.Insert(0, new BytesModelBinderProvider());
            })
            .AddJsonOptions(opt => opt.JsonSerializerOptions.PropertyNamingPolicy = null);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebFeatures", Version = "v1" });

                //string xmlFileName = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                //string xmlFilePath = Path.Combine(AppContext.BaseDirectory, xmlFileName);

                //c.IncludeXmlComments(xmlFilePath, true);
            });

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie();

            // For Frontend (React.JS)
            //services.AddMemoryCache();
            //services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            //services.AddReact();
            //services.AddJsEngineSwitcher(options => options.DefaultEngineName = V8JsEngine.EngineName).AddV8();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "API V1");
            });

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseDefaultFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseMiddleware<ExceptionHandlingMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            //app.UseReact(config => { });
        }
    }
}
