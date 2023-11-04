using Microsoft.AspNetCore.Mvc;
using TransactionService.Services;
using TransactionService.ViewModels;

namespace Stone.PSP.Web.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CashOutController : ControllerBase
    {
        private readonly ICashOutService _cashOutService;

        public CashOutController(ICashOutService cashOutService)
        {
            _cashOutService = cashOutService;
        }

        [HttpGet]
        [Route("GetBalance")]
        public async Task<IActionResult> GetBalance([FromQuery] Guid clientId)
        {
            var balance = await _cashOutService.GetBalanceAsync(clientId);

            return Ok(balance);
        }
    }
}
