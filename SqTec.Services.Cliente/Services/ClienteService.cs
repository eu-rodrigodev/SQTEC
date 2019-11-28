using SqTec.Domain.Entities;
using SqTec.Domain.Repositories.Cliente;
using SqTec.Services.Config.Services;
using SqTec.Services.Log.Services;
using SqTec.Shared;
using SqTec.Spec.Entities;
using SqTec.Spec.Exceptions;
using SqTec.Spec.Services;
using SqtTec.Exceptions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace SqTec.Services.Cliente.Services
{
    /// <summary>
    /// Classe de serviço de Clientes
    /// </summary>
    public class ClienteService : IClienteService
    {
        #region props
        /// <summary>
        /// Lista de clientes
        /// </summary>
        private List<Customer> Clientes;

        /// <summary>
        /// Lista de clientes backup caso o método Rollback() precise ser executado
        /// </summary>
        private List<Customer> ClientesBkp;
        #endregion

        #region Implementation IClienteService
        public void BeginTransaction()
        {
            var caminhoJson = new ConfigService().ObterConfiguracao<string>(Consts.CaminhoArquivoDados);

            if (caminhoJson.IndexOfAny(Path.GetInvalidPathChars()) != -1 && new FileInfo(caminhoJson).Extension.ToUpper() != ".JSON")
                throw new CaminhoInvalidoException(caminhoJson);

            if (File.Exists(caminhoJson))
                ClientesBkp = Clientes = new ClienteRepository().GetLista();
            else
                ClientesBkp = Clientes = new List<Customer>();
        }

        public void Atualizar(ICliente cliente)
        {
            Customer obj = Clientes.Where(w => w.IdentificadorERP == cliente.IdentificadorERP).FirstOrDefault();

            if (obj != null)
            {
                obj.SetDataNascimento(cliente.DataNascimento);
                obj.SetNome(cliente.Nome);
                obj.SetPontos(cliente.Pontos);
                obj.SetRegiao(cliente.Regiao);
            }
        }

        public int CalcularIdade(ICliente cliente)
        {
            DateTime date = DateTime.Now;

            int idade = date.Year - cliente.DataNascimento.Year;
            if (date.DayOfYear < cliente.DataNascimento.DayOfYear)
                idade = idade--;

            return idade;
        }

        public void Commit()
        {
            new ClienteRepository().Salvar(Clientes);
        }

        public void Inserir(ICliente cliente)
        {
            Clientes.Add(cliente as Customer);
        }

        public IEnumerable<ICliente> Listar()
        {
            return Clientes.ToList<ICliente>();
        }

        public IEnumerable<ICliente> ObterClientesDeTxt(string caminho)
        {
            if (caminho.IndexOfAny(Path.GetInvalidPathChars()) != -1)
                throw new CaminhoInvalidoException(caminho);

            if (new FileInfo(caminho).Extension.ToUpper() != ".TXT")
                throw new CaminhoTxtException(caminho);

            if (!File.Exists(caminho))
                throw new TxtNaoEncontradoException();

            Customer cliente;

            List<Customer> ClientesTxt = new List<Customer>();

            foreach (var linha in File.ReadAllLines(caminho, Encoding.Default))
            {
                string[] sLinha = linha.Split(';');

                DateTime dNascimento;
                int iPontos;

                try
                {
                    if (string.IsNullOrWhiteSpace(linha) || sLinha.Where(w => string.IsNullOrWhiteSpace(w)).Any())
                        throw new LinhaInvalidaException(linha);

                    if (!DateTime.TryParse(sLinha[2], out dNascimento))
                        throw new ParseIncorretoException("Data de nascimento", sLinha[2]);

                    if (!int.TryParse(sLinha[4], out iPontos))
                        throw new ParseIncorretoException("Quantidade de pontos", sLinha[4]);

                    cliente = new Customer(sLinha[0], sLinha[1], dNascimento, sLinha[3], Convert.ToInt32(sLinha[4]));

                    if (cliente.Invalid)
                        throw new NotifiableException(cliente.Errors());

                    ClientesTxt.Add(cliente);
                }
                catch (Exception ex)
                {
                    new LogService().Log(ex);
                }
            }

            return ClientesTxt.ToList<ICliente>();
        }

        public ICliente ObterPorId(Guid identificadorErp)
        {
            return Clientes.Where(w => w.IdentificadorERP == identificadorErp).FirstOrDefault();
        }

        public void Rollback()
        {
            Clientes = ClientesBkp;
        }
        #endregion
    }
}
