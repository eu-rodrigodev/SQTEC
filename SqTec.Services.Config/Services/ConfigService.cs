using SqTec.Spec.Exceptions;
using SqTec.Spec.Services;
using SqtTec.Exceptions;
using System;
using System.Configuration;

namespace SqTec.Services.Config.Services
{
    /// <summary>
    /// Classe de serviço de Configura~ção
    /// </summary>
    public class ConfigService : IConfigService
    {
        public T ObterConfiguracao<T>(string chave)
        {
            if (string.IsNullOrWhiteSpace(chave))
                throw new ArgumentException("ConfigService.ObterConfiguracao - O parâmetro é nulo, vazio ou há somente espaços em branco", "mensagem");

            var configuracao = ConfigurationManager.AppSettings[chave];
            if (string.IsNullOrWhiteSpace(configuracao))
                throw new ConfiguracaoNaoEncontradaException(chave);
            try
            {
                return (T)Convert.ChangeType(configuracao, typeof(T));
            }
            catch (Exception)
            {
                throw new ParseIncorretoException("ConfigService.ObterConfiguracao");
            }
        }
    }
}
