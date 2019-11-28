using Newtonsoft.Json;
using SqTec.Domain.Entities;
using SqTec.Domain.Repositories.IRepositories;
using SqTec.Services.Config.Services;
using SqTec.Shared;
using SqTec.Spec.Exceptions;
using SqtTec.Exceptions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace SqTec.Domain.Repositories.Cliente
{
    /// <summary>
    /// Classe de repositório de Cliente
    /// </summary>
    public class ClienteRepository : IClienteRepository
    {
        /// <summary>
        /// Retorna lista de todos os clientes armazenados no arquivo JSon da solução
        /// </summary>
        /// <returns></returns>
        public List<Customer> GetLista()
        {
            var caminhoJson = new ConfigService().ObterConfiguracao<string>(Consts.CaminhoArquivoDados);

            var file = new FileInfo(caminhoJson);
            if (caminhoJson.IndexOfAny(Path.GetInvalidPathChars()) != -1 && file.Extension.ToUpper() != ".JSON")
                throw new CaminhoInvalidoException(caminhoJson);

            try
            {
                return JsonConvert.DeserializeObject<List<Customer>>(File.ReadAllText(caminhoJson, Encoding.UTF8));
            }
            catch (Exception ex)
            {
                throw new DeserializeJSonFileException(file.Name, ex);
            }
        }

        /// <summary>
        /// Grava lista de clientes no arquivo JSon da solução
        /// </summary>
        public void Salvar(List<Customer> entities)
        {
            var path = new ConfigService().ObterConfiguracao<string>(Consts.CaminhoArquivoDados);

            try
            {
                var json = JsonConvert.SerializeObject(from e in entities
                                                       select new
                                                       {
                                                           e.IdentificadorERP,
                                                           e.Nome,
                                                           e.DataNascimento,
                                                           e.Regiao,
                                                           e.Pontos
                                                       });

                using (StreamWriter str = new StreamWriter(path))
                {
                    str.Write(json);
                }
            }
            catch (Exception ex)
            {
                throw new SerializeJSonException(ex);
            }
        }
    }
}
