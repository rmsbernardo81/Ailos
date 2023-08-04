using Questao5.Domain.Enumerators;

namespace Questao5.Domain.Entities
{
    public class Movimento
    {
        public string MovimentoId { get; set; }
        public string ContaCorrenteId { get; set; }
        public string DataMovimento { get; set; }
        public string TipoMovimento { get; set; }
        public double Valor { get; set; }
    }
}
