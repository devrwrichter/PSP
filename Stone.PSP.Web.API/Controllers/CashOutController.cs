using Microsoft.AspNetCore.Mvc;
using System.Net;
using TransactionService.Services;
using TransactionService.ViewModels;

namespace Stone.PSP.Web.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CashOutController : MainController
    {
        private readonly ICashOutService _cashOutService;

        public CashOutController(ICashOutService cashOutService)
        {
            _cashOutService = cashOutService;
        }

        [HttpGet]
        [Route("GetBalance")]
        public async Task<IActionResult> GetBalanceAsync([FromQuery] Guid clientId)
        {
            try
            {
                var balance = await _cashOutService.GetBalanceAsync(clientId);

                if (balance != null)
                    return Ok(balance);
                else
                    return NotFound();
            }catch(Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, InternalServerError);
            }
        }
    }
}
