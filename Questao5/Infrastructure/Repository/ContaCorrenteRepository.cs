using Dapper;
using Questao5.Domain.Entities;
using Questao5.Domain.Enumerators;
using Questao5.Domain.Interfaces;

namespace Questao5.Infrastructure.Repository
{
    public class ContaCorrenteRepository : SqLiteBaseRepository, IContaCorrenteRepository
    {
        public ContaCorrente GetContaCorrente(string contaId)
        {
            if (!File.Exists(DbFile)) return null;

            using (var cnn = SimpleDbConnection())
            {
                cnn.Open();
                ContaCorrente result = cnn.Query<ContaCorrente>(
                    @"SELECT idcontacorrente, numero, nome, ativo
                    FROM contacorrente
                    WHERE idcontacorrente = @contaId", new { contaId }).FirstOrDefault();
                return result;
            }
        }

        public double GetValor(string contaId)
        {
            if (!File.Exists(DbFile)) return 0;

            using (var cnn = SimpleDbConnection())
            {
                cnn.Open();
                var result = cnn.Query<Movimento>(@"SELECT * FROM movimento  WHERE idcontacorrente = @contaId", new { contaId }).AsList();
                var valorCredito = result.Where(e => e.TipoMovimento == "C").Sum(e => e.Valor);
                var valorDebito = result.Where(e => e.TipoMovimento == "D").Sum(e => e.Valor);
                return valorCredito - valorDebito;
            }
        }
    }
}
