using MediatR;
using Questao5.Application.Commands.Responses;
using Questao5.Domain.Enumerators;

namespace Questao5.Application.Commands.Requests
{
    public class CreateMovimentoCommandRequest : IRequest<CreateMovimentoCommandResponse>
    {
        public string ContaCorrenteId { get; set; }
        public string TipoMovimento { get; set; }
        public double Valor { get; set; }
    }
}
