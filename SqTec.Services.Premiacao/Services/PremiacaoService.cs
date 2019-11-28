using SqTec.Spec.Entities;
using SqTec.Spec.Services;

namespace SqTec.Services.Premiacao.Services
{
    public class PremiacaoService : IPremiacaoService
    {
        public PremiacaoService() { }

        public double CalcularDesconto(ICliente cliente)
        {
            return 1;
        }

        public int CalcularMedalhasBronze(ICliente cliente)
        {
            return 1;
        }

        public int CalcularMedalhasOuro(ICliente cliente)
        {
            return 1;
        }

        public int CalcularMedalhasPrata(ICliente cliente)
        {
            return 1;
        }
    }
}
