using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using PositionManagement.Application.Queries;
using PositionManagement.Infrastructure.UnitOfWork;

namespace PositionManagement.Application.Handlers
{
    public class GetPositionsHandler : IRequestHandler<GetPositionsQuery, Dictionary<string, int>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetPositionsHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Dictionary<string, int>> Handle(GetPositionsQuery request, CancellationToken cancellationToken)
        {
            var trades = await _unitOfWork.Trades.GetAllAsync();
            var grouped = trades
                .GroupBy(t => t.TradeId)
                .Select(g => g.OrderByDescending(t => t.Version).First());

            var result = new Dictionary<string, int>();
            foreach (var trade in grouped)
            {
                var isBuy = string.Equals(trade.Type, "BUY", StringComparison.OrdinalIgnoreCase);
                var isCancel = string.Equals(trade.Action, "CANCEL", StringComparison.OrdinalIgnoreCase);
                int sign = isCancel ? 0 : (isBuy ? 1 : -1);

                if (!result.ContainsKey(trade.SecurityCode))
                    result[trade.SecurityCode] = 0;
                result[trade.SecurityCode] += trade.Quantity * sign;
            }

            return result;
        }
    }
}
