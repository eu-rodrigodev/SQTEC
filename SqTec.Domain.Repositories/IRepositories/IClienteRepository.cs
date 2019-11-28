using SqTec.Domain.Entities;
using System.Collections.Generic;

namespace SqTec.Domain.Repositories.IRepositories
{
    /// <summary>
    /// Interface com assinatura dos métodos do repositório
    /// </summary>
    public interface IClienteRepository
    {
        void Salvar(List<Customer> entities);
        List<Customer> GetLista();
    }
}
