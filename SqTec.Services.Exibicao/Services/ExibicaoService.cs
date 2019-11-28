using SqTec.Spec.Dtos;
using SqTec.Spec.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SqTec.Services.Exibicao.Services
{
    public class ExibicaoService : IExibicaoService
    {
        public IEnumerable<RegiaoExibicao> AgruparClientesExibicaoPorRegiao(IEnumerable<ClienteExibicao> clientes)
        {
            return null;
        }

        public void ExibirClientes(IEnumerable<ClienteExibicao> clientes)
        {
            Console.WriteLine("========= Dados dos Clientes =========");

            var total = clientes.OrderByDescending(o => o.Nome.Length).First().Nome.Length.ToString();

            clientes.OrderBy(o => o.Nome).ToList().ForEach(c =>
            {
                Console.WriteLine(string.Format("{0, -" + total + "} | {1, -" + total + "}", c.Nome, c.Regiao));
            });
        }

        public void ExibirSumarizadoPorRegiao(IEnumerable<RegiaoExibicao> regioes)
        {

        }
    }
}
