using Questao5.Domain.Entities;

namespace Questao5.Domain.Interfaces
{
    public interface IMovimentoRepository
    {
        Movimento AddMovimento(Movimento dados);
        Movimento GetMovimento(string movimentoId);
    }
}
