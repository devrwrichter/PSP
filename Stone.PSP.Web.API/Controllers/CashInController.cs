using Microsoft.AspNetCore.Mvc;
using TransactionService.Services;
using TransactionService.ViewModels;

namespace Stone.PSP.Web.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CashInController : ControllerBase
    {
        private readonly ICashInService _cashInService;

        public CashInController(ICashInService cashInService)
        {
            _cashInService = cashInService;
        }
       
        [HttpPost]
        [Route("ProcessTransaction")]        
        public async Task<IActionResult> ProcessTransaction([FromBody] TransactionViewModel transaction)
        {
            var result = await _cashInService.ProcessTransactionAsync(transaction);

            return Ok();//TODO
        }
        
       
    }
}
