using SqTec.Services.Cliente.Services;
using SqTec.Services.Config.Services;
using SqTec.Services.Exibicao.Services;
using SqTec.Services.Log.Services;
using SqTec.Services.Premiacao.Services;
using SqTec.Spec.IoC;
using SqTec.Spec.Services;

namespace SqTec.Console
{
    public static class IoC
    {
        public static Container ObterContainer()
        {
            var container = Container.Inicializar();

            // Registre suas dependências aqui...

            container.Registrar<IClienteService, ClienteService>();
            container.Registrar<ILogService, LogService>();
            container.Registrar<IExibicaoService, ExibicaoService>();
            container.Registrar<IPremiacaoService, PremiacaoService>();
            container.Registrar<IConfigService, ConfigService>();
            container.Registrar<Sistema>();

            return container;
        }
    }
}
