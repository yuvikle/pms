using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using PositionManagement.Application.Commands;
using PositionManagement.Application.Validator;
using PositionManagement.Domain.Entities;
using PositionManagement.Infrastructure.UnitOfWork;

namespace PositionManagement.Application.Handlers
{
    public class CreateTradeHandler : IRequestHandler<CreateTradeCommand, string>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITradeValidator _tradeValidator;

        public CreateTradeHandler(IMapper mapper, IUnitOfWork iunitOfWork, ITradeValidator tradeValidator)
        {
            _mapper = mapper;
            _unitOfWork = iunitOfWork;
            _tradeValidator = tradeValidator;
        }

        public async Task<string> Handle(CreateTradeCommand request, CancellationToken cancellationToken)
        {
            var tradeValidation = await _tradeValidator.ValidateAsync(request);
            if (!tradeValidation.IsValid) return tradeValidation.ErrorMessage;

            var trade = _mapper.Map<Trade>(request);
            trade.Version = await _unitOfWork.Trades.GetTradeVersionAsync(trade);
            await _unitOfWork.Trades.AddAsync(trade);
            await _unitOfWork.CommitAsync(cancellationToken);

            return string.Empty;
        }
    }
}
