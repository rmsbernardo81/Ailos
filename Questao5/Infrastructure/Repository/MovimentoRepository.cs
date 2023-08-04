using Dapper;
using Questao5.Domain.Entities;
using Questao5.Domain.Interfaces;

namespace Questao5.Infrastructure.Repository
{
    public class MovimentoRepository : SqLiteBaseRepository, IMovimentoRepository
    {
        public Movimento AddMovimento(Movimento dados)
        {
            if (File.Exists(DbFile))
            {
                dados.MovimentoId = Guid.NewGuid().ToString();
                dados.DataMovimento = DateTime.Now.ToString("dd/MM/yyyy");
                using (var cnn = SimpleDbConnection())
                {
                    cnn.Open();
                    var sql = string.Format("INSERT INTO movimento (idmovimento, idcontacorrente, datamovimento, tipomovimento, valor) VALUES ('{0}', '{1}', '{2}', '{3}', {4})",
                      dados.MovimentoId,
                      dados.ContaCorrenteId,
                      dados.DataMovimento,
                      dados.TipoMovimento,
                      dados.Valor);
                    cnn.Execute(sql);
                }
            }

            return dados;
        }

        public Movimento GetMovimento(string movimentoId)
        {
            if (!File.Exists(DbFile)) return null;

            using (var cnn = SimpleDbConnection())
            {
                cnn.Open();
                return cnn.Query<Movimento>(@"SELECT * FROM movimento WHERE idmovimento = @movimentoId", new { movimentoId }).FirstOrDefault();
            }
        }
    }
}
