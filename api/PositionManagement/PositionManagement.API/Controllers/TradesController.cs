using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PositionManagement.Application.Commands;
using PositionManagement.Application.Queries;
using PositionManagement.Domain.Entities;

namespace PositionManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TradesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TradesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("positions")]
        public async Task<IActionResult> GetPositions()
        {
            var positions = await _mediator.Send(new GetPositionsQuery());
            return Ok(positions);
        }

        [HttpPost]
        public async Task<IActionResult> Post(CreateTradeCommand command)
        {
            var tradeResponse = await _mediator.Send(command);
            if(!string.IsNullOrWhiteSpace(tradeResponse))
            {
                return BadRequest(tradeResponse);
            }

            return Ok();
        }
    }
}
