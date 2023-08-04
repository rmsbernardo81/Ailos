using MediatR;
using Microsoft.EntityFrameworkCore;
using Questao5.Application.Queries.Requests;
using Questao5.Application.Queries.Responses;
using Questao5.Domain.Interfaces;
using Questao5.Infrastructure.Sqlite;

namespace Questao5.Application.Handlers
{
    public class ContaCorrenteHandler : IRequestHandler<ContaCorrenteQueryRequest, ContaCorrenteQueryResponse>
    {
        private readonly IContaCorrenteRepository _contaCorrenteRepository;

        public ContaCorrenteHandler(IContaCorrenteRepository contaCorrenteRepository)
        {
            _contaCorrenteRepository = contaCorrenteRepository;
        }

        public async Task<ContaCorrenteQueryResponse> Handle(ContaCorrenteQueryRequest request, CancellationToken cancellationToken)
        {
            var conta = _contaCorrenteRepository.GetContaCorrente(request.ContaId);

            if (conta == null)
            {
                throw new ApplicationException("Apenas contas correntes cadastradas podem consultar o saldo: [INVALID_ACCOUNT]");
            }

            if (conta.Ativo == Domain.Enumerators.TipoConta.INATIVO)
            {
                throw new ApplicationException("Apenas contas correntes ativas podem consultar o saldo: [INACTIVE_ACCOUNT]");
            }

            return new ContaCorrenteQueryResponse()
            {
                Numero = conta.Numero,
                Nome = conta.Nome,
                DataHora = DateTime.Now,
                Valor = _contaCorrenteRepository.GetValor(request.ContaId)
            };
        }
    }
}
