using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;
using System.Net.Security;
using Talabat.Apis.Errors;
using Talabat.Apis.Extensions;
using Talabat.Apis.Helpers;
using Talabat.Apis.MiddleWares;
using Talabat.Core.Entities.Identities;
using Talabat.Core.IRepository;
using Talabat.Repository;
using Talabat.Repository.Data;
using Talabat.Repository.Identity;
using Microsoft.ML.Data;
using Microsoft.Extensions;
using Microsoft.Extensions.ML;
using Talabat.Services.AI;

namespace Talabat.Apis
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddSwaggerServices();

            builder.Services.AddDbContext<StoreContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefualtConnection"));
            });
            builder.Services.AddDbContext<AppIdentityDbContext>(optionsBuilder =>
            {
                optionsBuilder.UseSqlServer(builder.Configuration.GetConnectionString("IdentityConnection"));
            });
            builder.Services.AddIdentity<AppUser, IdentityRole>(options =>
            {

            }).AddEntityFrameworkStores<AppIdentityDbContext>(); ;
            builder.Services.AddApplicationServices();
            builder.Services.AddPredictionEnginePool<ModelInput, ModelOutput>()
    .FromFile(modelName: "SentimentAnalysisModel", filePath: "sentiment_model.zip", watchForChanges: true);

            builder.Services.AddSingleton<IConnectionMultiplexer>(options =>
            {
                var Connection = builder.Configuration.GetConnectionString("Redis");
                return ConnectionMultiplexer.Connect(Connection);
            });
            
            var app = builder.Build();

            using var scoped =app.Services.CreateScope();
            var services = scoped.ServiceProvider;  
            var _dbcontext=services.GetRequiredService<StoreContext>();
            var _IdentityDBContect = services.GetRequiredService<AppIdentityDbContext>();
            var loggerFactor = services.GetRequiredService<ILoggerFactory>();
            try
            {
                await _dbcontext.Database.MigrateAsync();
                await StoreContextSeed.SeedAsync(_dbcontext);
                await _IdentityDBContect.Database.MigrateAsync();
                var _UserManger = services.GetRequiredService<UserManager<AppUser>>();
                await AppIdentityDbContextDataSeeding.UserAddAsync(_UserManger);
                
            }
           catch (Exception ex)
            {
                var logger =loggerFactor.CreateLogger<Program>();
                logger.LogError(ex, "an error has happened as soon as update data base");
            }


            // Configure the HTTP request pipeline.
            app.UseMiddleware<ExceptionMeddleWare>();
            if (app.Environment.IsDevelopment())
            {
                app.useSwagger();
            }
            app.UseStatusCodePagesWithReExecute("/error/{0}");
            app.UseHttpsRedirection();

            app.UseAuthorization();
            app.UseStaticFiles();

            app.MapControllers();

            app.Run();
        }
    }
}