using Microsoft.EntityFrameworkCore;
using PositionManagement.Application.Commands;
using PositionManagement.Infrastructure.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PositionManagement.Application.Validator
{
    public class TradeValidator : ITradeValidator
    {
        private readonly IUnitOfWork _unitOfWork;

        public TradeValidator(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<(bool IsValid, string ErrorMessage)> ValidateAsync(CreateTradeCommand command)
        {
            if (string.IsNullOrWhiteSpace(command.SecurityCode))
                return (false, "Security Code is required.");

            if (command.Quantity <= 0)
                return (false, "Quantity must be greater than zero.");

            if (!string.Equals(command.Type, "Buy", StringComparison.OrdinalIgnoreCase) 
                && !string.Equals(command.Type, "Sell", StringComparison.OrdinalIgnoreCase))
                return (false, "Type must be 'Buy' or 'Sell'.");

            if (!string.Equals(command.Action, "INSERT", StringComparison.OrdinalIgnoreCase)
                && !string.Equals(command.Action, "UPDATE", StringComparison.OrdinalIgnoreCase)
                && !string.Equals(command.Action, "CANCEL", StringComparison.OrdinalIgnoreCase))
            {
                return (false, "Invalid Action. Allowed values: INSERT, UPDATE, CANCEL.");
            }

            if(string.Equals(command.Action, "INSERT", StringComparison.OrdinalIgnoreCase))
            {
                var existingTrades = await _unitOfWork.Trades.GetByTradeIdAsync(command.TradeId);
                if (existingTrades.Any(t => t.Action == "INSERT"))
                    return (false, "Trade cannot be inserted with existing trade id");
            }

            return(true, string.Empty);
        }
    }
}
