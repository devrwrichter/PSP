using Stone.PSP.Infra.Context;

namespace Stone.PSP.Web.API.Configurations
{
    public static class HealthChecksConfig
    {
        public static void ConfigureHealthChecks(this IServiceCollection services, ConfigurationManager configuration)
        {
            var connectionString = configuration.GetConnectionString("SqlServer");
            services.AddHealthChecks().AddSqlServer(connectionString: connectionString ?? string.Empty, name: "Instância do Banco de dados");

            services.AddHealthChecks().AddDbContextCheck<PaymentContext>();
        }
    }
}
