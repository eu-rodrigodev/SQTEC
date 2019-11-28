using SqTec.Spec.Dtos;
using SqTec.Spec.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SqTec.Services.Exibicao.Services
{
    /// <summary>
    /// Classe de serviço de Exibição de informações
    /// </summary>
    public class ExibicaoService : IExibicaoService
    {
        public IEnumerable<RegiaoExibicao> AgruparClientesExibicaoPorRegiao(IEnumerable<ClienteExibicao> clientes)
        {
            // No documento estava pedindo para sumarizar os campos agrupados pela região.
            // Mas no exemplo parecia estar contando a quantidade de clientes ouro/prata/bronze por região.
            // Se precisar ser em outra vizão é só seguir os passos de comentário abaixo:
            return clientes
                    .GroupBy(r => r.Regiao)
                    //// Para agrupamento sumarizado de todos os campos por região:
                    .Select(re => new RegiaoExibicao(re.First().Regiao, re.Sum(s => s.MedalhasOuro), re.Sum(s => s.MedalhasPrata), re.Sum(s => s.MedalhasBronze), re.Sum(s => s.ValorDesconto)));
                    //// Para agrupamento sumarizado do desconto e quantidade de clientes ouro/prata/bronze:
                    //.Select(re => new RegiaoExibicao(re.First().Regiao, re.Count(s => s.MedalhasOuro > 0), re.Count(s => s.MedalhasPrata > 0), re.Count(s => s.MedalhasBronze > 0), re.Sum(s => s.ValorDesconto)));
        }

        public void ExibirClientes(IEnumerable<ClienteExibicao> clientes)
        {
            Console.WriteLine(" Dados dos Clientes::::" + Environment.NewLine);

            // contagem do tamanho dos maiores dados para organizar a exibição
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

            // contagem do tamanho dos maiores dados para organizar a exibição
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
