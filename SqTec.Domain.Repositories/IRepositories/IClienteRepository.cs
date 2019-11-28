using SqTec.Domain.Entities;
using SqTec.Spec.Entities;
using System.Collections.Generic;

namespace SqTec.Domain.Repositories.IRepositories
{
    public interface IClienteRepository
    {
        void Salvar(List<Customer> entities);
        List<Customer> GetLista();
    }
}
