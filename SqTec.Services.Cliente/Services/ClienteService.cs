using SqTec.Domain.Entities;
using SqTec.Domain.Repositories.Cliente;
using SqTec.Services.Config.Services;
using SqTec.Services.Log.Services;
using SqTec.Shared;
using SqTec.Spec.Entities;
using SqTec.Spec.Exceptions;
using SqTec.Spec.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace SqTec.Services.Cliente.Services
{
    public class ClienteService : IClienteService
    {
        #region props
        private List<Customer> Clientes;
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

            if (obj != null) obj = cliente as Customer;
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
                    if (!DateTime.TryParse(sLinha[2], out dNascimento))
                        throw new LinhaInvalidaException(linha);

                    if (!int.TryParse(sLinha[4], out iPontos))
                        throw new LinhaInvalidaException(linha);

                    cliente = new Customer(sLinha[0], sLinha[1], dNascimento, sLinha[3], Convert.ToInt32(sLinha[4]));

                    if (cliente.Invalid)
                        throw new LinhaInvalidaException(linha);

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
