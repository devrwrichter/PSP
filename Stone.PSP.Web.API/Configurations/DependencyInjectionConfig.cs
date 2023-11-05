using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Stone.PSP.Domain.Entities;
using Stone.PSP.Domain.Repositories;
using Stone.PSP.Domain.Services;
using Stone.PSP.Domain.UnitOfWork;
using Stone.PSP.Domain.Validators;
using Stone.PSP.Infra.Context;
using Stone.PSP.Infra.Repositories;
using Stone.PSP.Infra.UnitOfWork;
using Stone.PSP.TransactionService.Automapper;
using TransactionService.Services;

namespace Stone.PSP.Web.API.Configurations
{
    public static class DependencyInjectionConfig
    {
        public static void RegisterServices(this IServiceCollection services, ConfigurationManager configuration)
        {
            services.AddScoped<ICashInService, CashInService>();
            services.AddScoped<ICashOutService, CashOutService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IValidator<PspTransaction>, TransactionValidator>();
            services.AddScoped<IPspTransactionRepository, PspTransactionRepository>();
            services.AddScoped<IPayableRepository, PayableRepository>();
            services.AddScoped<IDatabaseTransaction, EntityDatabaseTransaction>();

            services.AddSingleton<IFeeConfigurationCacheService, FeeConfigurationCacheService>();

            CinfigAutomapper(services);
            ConfigDatabase(services, configuration);
        }

        private static void CinfigAutomapper(IServiceCollection services)
        {
            IMapper mapper = AutomapperConfig.GetConfiguration().CreateMapper();
            services.AddSingleton(mapper);
        }

        private static void ConfigDatabase(IServiceCollection services, ConfigurationManager configuration)
        {
            var connectionString = configuration.GetConnectionString("SqlServer");
            var optionsBuilder = new DbContextOptionsBuilder<PaymentContext>();
            optionsBuilder.UseSqlServer(connectionString);

            services.AddScoped<PaymentContext>(opt => new PaymentContext(optionsBuilder.Options));
            services.AddSingleton<PaymentDapperContext>();
        }
    }
}
