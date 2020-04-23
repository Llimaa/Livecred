using Livecred.Domain.Enuns;
using Livecred.Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Livecred.Domain.Repositories
{
    public interface IRepositoryParcela
    {
        Task Create(Parcela parcela);
        Task UpdateValor(Guid id, decimal valor);
        Task UpdateJuro(Guid id, double valor);
        Task UpdateDataVencimento(Guid id, DateTime dataVencimento);
        Task Delete(Guid id);
        Task<Parcela> GetById(Guid id);
        Task<Parcela> GetByStatus(EStatusParcela status);
        Task<Parcela> GetByPeriodo(DateTime periodo);
        Task<IEnumerable<Parcela>> GetAll();
        Task<IEnumerable<Parcela>> GetAllByIdLoan(Guid IdLoan);
    }
}
