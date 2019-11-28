using Flunt.Validations;
using SqTec.Domain.Core.Models;
using SqTec.Spec.Entities;
using System;

namespace SqTec.Domain.Entities
{
    public class Customer : Entity, ICliente
    {
        public Guid IdentificadorERP { get; private set; }

        public string Nome { get; private set; }

        public DateTime DataNascimento { get; private set; }

        public string Regiao { get; private set; }

        public int Pontos { get; private set; }

        public Customer(string identificadorERP, string nome, DateTime dataNascimento, string regiao, int pontos)
        {
            SetNome(nome);
            SetDataNascimento(dataNascimento);
            SetRegiao(regiao);
            SetPontos(pontos);

            AddNotifications(new Contract()
                .IsTrue(ValidateGuid(identificadorERP), "Cliente.IdentificadorERP", "Identificador inválido")
            );

            if (Valid)
                IdentificadorERP = Guid.Parse(identificadorERP);
        }

        #region sets
        public void SetNome(string nome)
        {
            AddNotifications(new Contract()
                .HasMinLen(nome, 2, "Cliente.Nome", "Mínimo de 2 caracteres")
                .HasMaxLen(nome, 60, "Cliente.Nome", "Máximo de 60 caracteres")
            );

            Nome = nome;
        }

        public void SetDataNascimento(DateTime dataNascimento)
        {
            AddNotifications(new Contract()
                .IsLowerOrEqualsThan(dataNascimento, DateTime.Now.AddDays(-1), "Cliente.DataNascimento", "O aniversário do cliente deve ser menor do que hoje")
            );

            DataNascimento = dataNascimento;
        }

        public void SetRegiao(string regiao)
        {
            AddNotifications(new Contract()
                .HasMinLen(regiao, 2, "Cliente.Regiao", "A sigla do estado possui 2 caracteres")
                .HasMaxLen(regiao, 2, "Cliente.Regiao", "A sigla do estado possui 2 caracteres")
            );

            Regiao = regiao;
        }

        public void SetPontos(int pontos)
        {
            AddNotifications(new Contract()
                .IsLowerThan(0, pontos, "Cliente.Pontos", "Não deve ser menor que zero")
            );

            Pontos = pontos;
        }
        #endregion

        #region private props/methods
        private bool ValidateGuid(string guid)
        {
            Guid GuidIdERP;
            return Guid.TryParse(guid, out GuidIdERP);
        }
        #endregion
    }
}
