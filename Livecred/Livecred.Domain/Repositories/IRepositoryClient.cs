using Livecred.Domain.Models;
using System;
using System.Collections.Generic;

namespace Livecred.Domain.Repositories
{
    public interface IRepositoryClient
    {
        void Create(Client client);
        void Update(Client client);
        void Delete(Guid guid);
        Client GetById(Guid guid);
        IEnumerable<Client> GetAll();
    }
}
