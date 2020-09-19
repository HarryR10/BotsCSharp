using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using FinanceBot.Models;
using FinanceBot.Models.Repository;

namespace FinanceBot
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            Configuration.Get<Settings>();

            services.AddSingleton(new Bot());

            services.AddTransient<IExpenseRepository, ExpenseInMemoryRepository>();
            services.AddTransient<IUserAccountRepository, UserAccountInMemoryRepository>();
            services.AddTransient<ICategoryRepository, CategoryInMemoryRepository>();

            services
                .AddControllers()
                .AddNewtonsoftJson(); 
        }

        public void Configure(IApplicationBuilder app,
            IWebHostEnvironment env,
            Bot bot)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseCors();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            //init webhook
            bot.Get(Configuration.Get<Settings>()).Wait();
        }
    }
}
