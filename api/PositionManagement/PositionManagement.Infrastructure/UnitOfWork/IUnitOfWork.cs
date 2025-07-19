using PositionManagement.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PositionManagement.Infrastructure.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        ITradeRepository Trades { get; }
        Task<int> CommitAsync(CancellationToken cancellationToken = default);
    }
}
