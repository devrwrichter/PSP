using Microsoft.OpenApi.Models;
using Stone.PSP.Web.API.Swagger;
using Swashbuckle.AspNetCore.Filters;

namespace Stone.PSP.Web.API.Configurations
{
    public static class SwaggerConfig
    {
        public static void ConfigureSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c => {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Payment Service Provider"
                });
                c.ExampleFilters();
            });
            services.AddSwaggerExamplesFromAssemblyOf<TransactionViewModelExample>();
        }
    }
}
