using Livecred.Domain.Enuns;
using Livecred.Domain.Models;
using System;
using System.Collections.Generic;
namespace Livecred.Domain.Repositories
{
    public interface IRepositoryParcela
    {
        void Create(Parcela parcela);
        void Update(Parcela parcela);
        void Delete(Guid id);
        Parcela GetById(Guid id);
        Parcela GetByStatus(EStatusParcela status);
        Parcela GetByPeriodo(DateTime periodo);
        IEnumerable<Parcela> GetAll();
    }
}
