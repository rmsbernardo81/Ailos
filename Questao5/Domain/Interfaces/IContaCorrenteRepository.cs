using Questao5.Domain.Entities;
using Questao5.Domain.Enumerators;

namespace Questao5.Domain.Interfaces
{
    public interface IContaCorrenteRepository
    {
        ContaCorrente GetContaCorrente(string contaId);
        double GetValor(string contaId);
    }
}
