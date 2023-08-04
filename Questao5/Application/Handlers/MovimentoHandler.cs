using MediatR;
using Questao5.Application.Commands.Requests;
using Questao5.Application.Commands.Responses;
using Questao5.Application.Mapper;
using Questao5.Domain.Entities;
using Questao5.Domain.Enumerators;
using Questao5.Domain.Interfaces;

namespace Questao5.Application.Handlers
{
    public class MovimentoHandler : IRequestHandler<CreateMovimentoCommandRequest, CreateMovimentoCommandResponse>
    {
        private readonly IContaCorrenteRepository _contaCorrenteRepository;
        private readonly IMovimentoRepository _movimentoRepository;

        public MovimentoHandler(IMovimentoRepository movimentoRepository, IContaCorrenteRepository contaCorrenteRepository)
        {
            _movimentoRepository = movimentoRepository;
            _contaCorrenteRepository = contaCorrenteRepository;
        }

        public async Task<CreateMovimentoCommandResponse> Handle(CreateMovimentoCommandRequest request, CancellationToken cancellationToken)
        {
            var movimentoEntity = AppMapper.Mapper.Map<Movimento>(request);
            if (movimentoEntity.Valor <= 0)
            {
                throw new ApplicationException("Apenas valores positivos podem ser recebidos: [INVALID_VALUE]");
            }

            if (movimentoEntity.TipoMovimento != "C" && movimentoEntity.TipoMovimento != "D")
            {
                throw new ApplicationException("Apenas os tipos “débito” ou “crédito” podem ser aceitos: [INVALID_TYPE]");
            }           
            
            var conta = _contaCorrenteRepository.GetContaCorrente(request.ContaCorrenteId);
            if (conta == null)
            {
                throw new ApplicationException("Apenas contas correntes cadastradas podem receber movimentação: [INVALID_ACCOUNT]");
            }

            if (conta.Ativo == Domain.Enumerators.TipoConta.INATIVO)
            {
                throw new ApplicationException("Apenas contas correntes ativas podem receber movimentação: [INACTIVE_ACCOUNT]");
            }
            
            var newMovimento = _movimentoRepository.AddMovimento(movimentoEntity);
            var movimento = _movimentoRepository.GetMovimento(newMovimento.MovimentoId);

            if (movimento == null)
            {
                throw new ApplicationException("Conta não localizada: [INVALID_ACCOUNT]");
            }

            return new CreateMovimentoCommandResponse()
            {
                MovimentoId = newMovimento.MovimentoId
            };
        }
    }
}
