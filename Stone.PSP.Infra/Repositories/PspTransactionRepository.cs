﻿using Dapper;
using Microsoft.EntityFrameworkCore;
using Stone.PSP.Crosscutting;
using Stone.PSP.Domain.Entities;
using Stone.PSP.Domain.Repositories;
using Stone.PSP.Infra.Context;

namespace Stone.PSP.Infra.Repositories
{
    public class PspTransactionRepository : IPspTransactionRepository
    {
        private readonly PaymentContext _context;
        private readonly PaymentDapperContext _dapperContext;

        public PspTransactionRepository(PaymentContext context, PaymentDapperContext dapperContext)
        {
            _context = context;
            _dapperContext = dapperContext;
        }

        public async Task<PspTransaction?> GetTransactionByIdAsync(Guid id)
        {
            return await _context.Transactions
                .Where(x => x.Id == id)
                .AsNoTracking()
                .TagWith(nameof(GetTransactionByIdAsync))
                .FirstOrDefaultAsync();
        }

        private async Task<int> GetTransactionsCount()
        {
            var query = @"SELECT COUNT(1) as Total from Transactions";

            using (var connection = _dapperContext.CreateConnection())
            {
                return await connection.ExecuteScalarAsync<int>(query);
            }
        }

        public async Task<IEnumerable<PspTransaction>> GetTransactionsAsync()
        {
            IEnumerable<PspTransaction> items = Enumerable.Empty<PspTransaction>();

            var query = @"SELECT    * 
                            FROM    dbo.Transactions";

            using (var connection = _dapperContext.CreateConnection())
            {
                return await connection.QueryAsync<PspTransaction>(query);
            }
        }

        public async Task<(int Count, IEnumerable<PspTransaction> Items)> GetTransactionsAsync(PaginationRequest pagination)
        {
            IEnumerable<PspTransaction> items = Enumerable.Empty<PspTransaction>();

            var itemsCount = await GetTransactionsCount();

            var query = @"SELECT    * 
                            FROM    dbo.Transactions T
                        ORDER BY    T.Id
                          OFFSET    (@pageNumber - 1) * @pageSize
                 ROWS FETCH NEXT    @pageSize ROWS ONLY";

            using (var connection = _dapperContext.CreateConnection())
            {
                items = await connection.QueryAsync<PspTransaction>(query, new { pageSize = pagination.PageSize, pageNumber = pagination.PageNumber });
            }

            return (itemsCount, items);
        }

        public async Task SaveAsync(PspTransaction pspTransaction)
        {
            await _context.Transactions.AddAsync(pspTransaction);
            await _context.SaveChangesAsync();
        }


    }
}
