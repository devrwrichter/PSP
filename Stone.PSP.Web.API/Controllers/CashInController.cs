using Microsoft.AspNetCore.Mvc;
using Stone.PSP.Crosscutting;
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

        [HttpGet]
        [Route("GetById")]
        public async Task<IActionResult> GetById(Guid id) {

            var transaction = await _cashInService.GetTransactionByIdAsync(id);
            if(transaction != null)
                return Ok(transaction);
            else 
                return NotFound();
        }

        [HttpGet]
        [Route("GetTransactions")]
        public async Task<IActionResult> GetTransactions(Pagination pagination)
        {
            var result = await _cashInService.GetTransactionsAsync(pagination);
            if (result.Any())
                return Ok(result);
            else
                return NotFound();
        }

        [HttpPost]
        [Route("ProcessTransaction")]        
        public async Task<IActionResult> ProcessTransaction([FromBody] TransactionViewModel transaction)
        {
            var result = await _cashInService.ProcessTransactionAsync(transaction);
            if (!result.Success)
                return BadRequest(result);

            return new CreatedAtActionResult(nameof(GetById), "CashIn", new { id = result.Model?.TransactionId }, result.Model);
        }
    }
}
