using SqTec.Extensions;
using SqTec.Services.Config.Services;
using SqTec.Shared;
using SqTec.Spec.Services;
using System;
using System.IO;
using System.Text;

namespace SqTec.Services.Log.Services
{
    /// <summary>
    /// Classe de serviço de Log de erros
    /// </summary>
    public class LogService : ILogService
    {
        public void Log(string mensagem)
        {
            var pathArquivoLog = new ConfigService().ObterConfiguracao<string>(Consts.CaminhoArquivoLog);
            
            using (var sw = new StreamWriter(pathArquivoLog, true, Encoding.Unicode))
            {
                sw.WriteLine(string.Format("{0} | {1}", DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"), mensagem.Replace(Environment.NewLine, " ")));
                sw.Close();
            }
        }

        public void Log(Exception ex)
        {
            Log(ex.GetRecursiveErrorMessage());
        }
    }
}
