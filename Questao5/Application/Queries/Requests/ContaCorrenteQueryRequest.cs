using MediatR;
using Questao5.Application.Queries.Responses;

namespace Questao5.Application.Queries.Requests
{
    public class ContaCorrenteQueryRequest : IRequest<ContaCorrenteQueryResponse>
    {
        public int Numero { get; set; }
        public string Nome { get; set; }
        public string ContaId { get; set; }

        public ContaCorrenteQueryRequest(string idConta)
        {
            this.ContaId = idConta;
        }
    }
}
