using MediatR;
using Microsoft.AspNetCore.Mvc;
using Questao5.Application.Commands.Requests;
using Questao5.Application.Queries.Requests;
using Questao5.Application.Queries.Responses;

namespace Questao5.Infrastructure.Services.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ContaCorrenteController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ContaCorrenteController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{contaId}")]
        public async Task<ActionResult<IEnumerable<ContaCorrenteQueryResponse>>> GetSaldoContaCorrente(string contaId)
        {
            try
            {
                var result = await _mediator.Send(new ContaCorrenteQueryRequest(contaId), CancellationToken.None);
                return Ok(result);
            }
            catch (System.Exception ex)
            {
                return StatusCode(400, ex.Message);
            }
        }

        [HttpPost("AddMovimentacao")]
        public async Task<ActionResult<IEnumerable<ContaCorrenteQueryResponse>>> AddMovimentacao([FromBody] CreateMovimentoCommandRequest command)
        {
            try
            {
                var result = await _mediator.Send(command, CancellationToken.None);
                return Ok(result);
            }
            catch (System.Exception ex)
            {
                return StatusCode(400, ex.Message);
            }
        }
    }
}
