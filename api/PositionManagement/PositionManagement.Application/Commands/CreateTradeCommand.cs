using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace PositionManagement.Application.Commands
{
    public record CreateTradeCommand(int TradeId, 
        string SecurityCode, int Quantity, 
        string Action, string Type) : IRequest<string>;
}
