using SqTec.Services.Cliente.Services;
using SqTec.Spec.Entities;
using SqTec.Spec.Services;

namespace SqTec.Services.Premiacao.Services
{
    /// <summary>
    /// Classe de serviço de Premiação, calculos por faixa de prêmio e desconto
    /// </summary>
    public class PremiacaoService : IPremiacaoService
    {
        public int CalcularMedalhasOuro(ICliente cliente)
        {
            return cliente.Pontos / 10000;
        }

        public int CalcularMedalhasPrata(ICliente cliente)
        {
            return (cliente.Pontos - (CalcularMedalhasOuro(cliente) * 10000)) / 1000;
        }

        public int CalcularMedalhasBronze(ICliente cliente)
        {
            return (cliente.Pontos - (CalcularMedalhasOuro(cliente) * 10000) - (CalcularMedalhasPrata(cliente) * 1000)) / 100;
        }

        public double CalcularDesconto(ICliente cliente)
        {
            var ouro = CalcularMedalhasOuro(cliente);
            var metade = ouro / 2;
            var idade = new ClienteService().CalcularIdade(cliente);
            var retorno = ((Fatorial(ouro) / (Fatorial(metade) * Fatorial(ouro - metade))) + (2 * idade)) / 100.00;

            return retorno > 30 ? 30 : retorno;
        }

        #region private methods
        private long Fatorial(int n)
        {
            return n <= 1 ? 1 : n * Fatorial(n - 1);
        }
        #endregion
    }
}
