using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PositionManagement.Application.Queries
{
    public record GetPositionsQuery : IRequest<Dictionary<string, int>>;
}
