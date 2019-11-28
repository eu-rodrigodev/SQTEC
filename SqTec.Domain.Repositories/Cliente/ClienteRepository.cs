using Newtonsoft.Json;
using SqTec.Domain.Entities;
using SqTec.Domain.Repositories.IRepositories;
using SqTec.Services.Config.Services;
using SqTec.Shared;
using SqTec.Spec.Exceptions;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace SqTec.Domain.Repositories.Cliente
{
    public class ClienteRepository : IClienteRepository
    {
        public List<Customer> GetLista()
        {
            var caminhoJson = new ConfigService().ObterConfiguracao<string>(Consts.CaminhoArquivoDados);

            if (caminhoJson.IndexOfAny(Path.GetInvalidPathChars()) != -1 && new FileInfo(caminhoJson).Extension.ToUpper() != ".JSON")
                throw new CaminhoInvalidoException(caminhoJson);

            return JsonConvert.DeserializeObject<List<Customer>>(File.ReadAllText(caminhoJson, Encoding.UTF8));
        }

        public void Salvar(List<Customer> entities)
        {
            var path = new ConfigService().ObterConfiguracao<string>(Consts.CaminhoArquivoDados);

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
    }
}
