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
            return clientes
                    .GroupBy(r => r.Regiao)
                    .Select(re => new RegiaoExibicao(re.First().Regiao, re.Sum(s => s.MedalhasOuro), re.Sum(s => s.MedalhasPrata), re.Sum(s => s.MedalhasBronze), re.Sum(s => s.ValorDesconto)));
        }

        public void ExibirClientes(IEnumerable<ClienteExibicao> clientes)
        {
            Console.WriteLine(" Dados dos Clientes::::" + Environment.NewLine);

            var maxlNome = clientes.OrderByDescending(o => o.Nome.Length).First().Nome.Length;
            var maxlIdade = clientes.OrderByDescending(o => o.Idade).First().Idade.ToString().Length;
            var maxlOuro = clientes.OrderByDescending(o => o.MedalhasOuro).First().MedalhasOuro.ToString().Length;
            var maxlPrata = clientes.OrderByDescending(o => o.MedalhasPrata).First().MedalhasPrata.ToString().Length;
            var maxlBronze = clientes.OrderByDescending(o => o.MedalhasBronze).First().MedalhasBronze.ToString().Length;
            var maxlDesconto = clientes.OrderByDescending(o => o.ValorDesconto).First().ValorDesconto.ToString().Length;

            clientes.OrderBy(o => o.Nome).ToList().ForEach(c =>
            {
                Console.WriteLine(string.Format(" {0, -" + maxlNome + "} | {1, -" + maxlIdade + "} anos | Ouro: {2, -" + maxlOuro + "} | Prata: {3, -" + maxlPrata + "} | Bronze: {4, -" + maxlBronze + "} | Desconto: {5, -" + maxlDesconto + "}",
                    c.Nome, c.Idade, c.MedalhasOuro, c.MedalhasPrata, c.MedalhasBronze, c.ValorDesconto.ToString("c")));
            });
        }

        public void ExibirSumarizadoPorRegiao(IEnumerable<RegiaoExibicao> regioes)
        {
            Console.WriteLine(" Dados Regionais::::" + Environment.NewLine);
            
            var maxlOuro = regioes.OrderByDescending(o => o.MedalhasOuro).First().MedalhasOuro.ToString().Length;
            var maxlPrata = regioes.OrderByDescending(o => o.MedalhasPrata).First().MedalhasPrata.ToString().Length;
            var maxlBronze = regioes.OrderByDescending(o => o.MedalhasBronze).First().MedalhasBronze.ToString().Length;
            var maxlDesconto = regioes.OrderByDescending(o => o.ValorDesconto).First().ValorDesconto.ToString().Length;

            regioes.OrderBy(o => o.Regiao).ToList().ForEach(c =>
            {
                Console.WriteLine(string.Format(" {0} | Ouro: {1, -" + maxlOuro + "} | Prata: {2, -" + maxlPrata + "} | Bronze: {3, -" + maxlBronze + "} | Desconto: {4, -" + maxlDesconto + "}",
                    c.Regiao, c.MedalhasOuro, c.MedalhasPrata, c.MedalhasBronze, c.ValorDesconto.ToString("c")));
            });
        }
    }
}
