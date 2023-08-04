namespace Questao1
{
    public class ContaBancaria
    {
        public int Numero { get; set; }
        public string Titular { get; set; }
        public double ValorConta { get; set; }

        public ContaBancaria(int numero, string titular, double depositoInicial = 0)
        {
            Numero = numero;
            Titular = titular;
            ValorConta = depositoInicial;
        }

        public void Deposito(double quantia)
        {
            ValorConta += quantia;
        }

        public void Saque(double quantia)
        {
            ValorConta -= (quantia + 3.5);
        }
    }
}
