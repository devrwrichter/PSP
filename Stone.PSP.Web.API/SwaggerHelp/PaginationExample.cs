using Stone.PSP.Crosscutting;
using Swashbuckle.AspNetCore.Filters;

namespace Stone.PSP.Web.API.SwaggerHelp
{
    public class PaginationExample : IExamplesProvider<PaginationRequest>
    {
        public PaginationRequest GetExamples()
        {
            return new PaginationRequest { PageNumber = 1, PageSize = 10 };
        }
    }
}
