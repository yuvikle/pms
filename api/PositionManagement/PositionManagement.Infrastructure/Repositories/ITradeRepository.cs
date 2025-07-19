using PositionManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PositionManagement.Infrastructure.Repositories
{
    public interface ITradeRepository : IRepository<Trade>
    {
        Task<List<Trade>> GetByTradeIdAsync(int tradeId);
        Task<int> GetTradeVersionAsync(Trade trade);
    }
}
