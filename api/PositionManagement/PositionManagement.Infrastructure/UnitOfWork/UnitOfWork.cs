using Microsoft.Extensions.DependencyInjection;
using PositionManagement.Infrastructure.Context;
using PositionManagement.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PositionManagement.Infrastructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly TradeContext _tradeContext;
        private readonly IServiceProvider _serviceProvider;

        private ITradeRepository? _trades;

        public UnitOfWork(TradeContext tradeContext, IServiceProvider serviceProvider)
        {
            _tradeContext = tradeContext;
            _serviceProvider = serviceProvider;

            Trades = _serviceProvider.GetRequiredService<ITradeRepository>();
        }

        public ITradeRepository Trades { get; }

        public async Task<int> CommitAsync(CancellationToken cancellationToken = default)
            => await _tradeContext.SaveChangesAsync(cancellationToken);

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _tradeContext?.Dispose();
            }
        }
    }
}
