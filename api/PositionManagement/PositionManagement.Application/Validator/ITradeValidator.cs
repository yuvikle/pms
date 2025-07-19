using PositionManagement.Application.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PositionManagement.Application.Validator
{
    public interface ITradeValidator
    {
        Task<(bool IsValid, string ErrorMessage)> ValidateAsync(CreateTradeCommand command);
    }
}
