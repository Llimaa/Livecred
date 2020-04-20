using Livecred.Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Livecred.Domain.Repositories
{
    public interface IRepositoryClient
    {
        Task Create(Client client);
        Task Update(Client client);
        Task Delete(Guid guid);
        Task<Client> GetById(Guid guid);
        Task<IEnumerable<Client>> GetAll();
    }
}
