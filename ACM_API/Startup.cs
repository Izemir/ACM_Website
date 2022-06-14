using ACM_API.DB;
using ACM_API.Services.AuthService;
using ACM_API.Services.ChatService;
using ACM_API.Services.CustomerService;
using ACM_API.Services.ExecutorService;
using ACM_API.Services.FileService;
using ACM_API.Services.ModeratorService;
using ACM_API.Services.OrderService;
using ACM_API.Services.SearchService;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace ACM_API.Startup
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                builder =>
                {
                    builder.WithOrigins("https://acm.qp.ru", "https://localhost:8001").AllowAnyMethod().AllowAnyHeader();
                });
                }
            );

            services.AddControllers();
            services.AddDbContext<DBContext>();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ACM_API", Version = "v1" });
            });
            services.AddAutoMapper(typeof(Startup));
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<IExecutorService, ExecutorService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<ISearchService, SearchService>();
            services.AddScoped<IModeratorService, ModeratorService>();
            services.AddScoped<IChatService, ChatService>();
            services.AddScoped<IFileService, FileService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddControllersWithViews()
                    .AddNewtonsoftJson(options =>
                    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                    );          

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment() || env.IsProduction()) //env.IsDevelopment()
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ACM_API v1"));
            }
            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });



            app.UseHttpsRedirection();

            app.UseRouting();

            //именно в этом месте
            app.UseCors();

            app.UseAuthorization();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
