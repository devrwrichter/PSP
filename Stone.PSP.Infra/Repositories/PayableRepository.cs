using Dapper;
using Stone.PSP.Domain.Entities;
using Stone.PSP.Domain.Repositories;
using Stone.PSP.Infra.Context;

namespace Stone.PSP.Infra.Repositories
{
    public class PayableRepository : IPayableRepository
    {
        private readonly PaymentContext _context;
        private readonly PaymentDapperContext _dapperContext;

        public PayableRepository(PaymentContext context, PaymentDapperContext dapperContext)
        {
            _context = context;
            _dapperContext = dapperContext;
        }

        public async Task<ClientBalance> GetBalanceAsync(Guid clientId)
        {
            var query = @"SELECT [1] as 'Available',[2] as 'WaitingFunds' FROM (
                          SELECT SUM(P.Value) AS Total, P.Status FROM Transactions T
                            INNER JOIN Payables P
                            ON (T.Id = P.TransactionId)
                            WHERE T.ClientId = @ClientId
                            GROUP by p.Status
                            )as T
                            PIVOT(
                            SUM(T.Total)
                            FOR T.status in ([1],[2])
                            )as Balance";

            using (var connection = _dapperContext.CreateConnection())
            {
                var result = await connection.QuerySingleAsync<ClientBalance>(query, new { clientId });
                return result;
            }
        }

        public async Task SaveAsync(Payable payable)
        {
            await _context.Payables.AddAsync(payable);
            await _context.SaveChangesAsync();
        }
    }
}
