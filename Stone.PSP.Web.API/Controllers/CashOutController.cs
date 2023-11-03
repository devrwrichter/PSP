using Microsoft.AspNetCore.Mvc;
using TransactionService.ViewModels;

namespace Stone.PSP.Web.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CashOutController : ControllerBase
    {
        [HttpGet]
        [Route("GetBalance")]
        public async Task<IActionResult> GetBalance([FromQuery] Guid clientId)
        {
            return Ok(new ClientBalanceViewModel());
        }
    }
}
