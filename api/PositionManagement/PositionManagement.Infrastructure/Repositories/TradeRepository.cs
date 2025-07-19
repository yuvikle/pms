using Microsoft.EntityFrameworkCore;
using PositionManagement.Domain.Entities;
using PositionManagement.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PositionManagement.Infrastructure.Repositories
{
    public class TradeRepository : Repository<Trade>, ITradeRepository
    {
        public TradeRepository(TradeContext tradeContext) : base(tradeContext) { }

        public async Task<List<Trade>> GetByTradeIdAsync(int tradeId) =>
            await _tradeContext.Trades
                .Where(t => t.TradeId == tradeId)
                .OrderByDescending(t => t.Version)
                .ToListAsync();

        public async Task<int> GetTradeVersionAsync(Trade trade)
        {
            var latestVersion = await _tradeContext.Trades
                .Where(t => t.TradeId == trade.TradeId)
                .MaxAsync(t => (int?)t.Version);
            return (latestVersion ?? 0) + 1;
        }
    }
}
