using Livecred.Domain.Enuns;
using Livecred.Domain.Models;
using Livecred.Domain.Queries;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Livecred.Domain.Repositories
{
    public interface IRepositoryLoan
    {
        Task Create(Loan loan);
        Task Update(Loan loan);
        Task Delete(Guid guid);
        Task<Loan> GetById(Guid id);
        Task<IEnumerable<Loan>> GetAll();
        Task<IEnumerable<Loan>> GetAllbyIdClient(Guid IdClient);
        Task<IEnumerable<Loan>> GetAllOrderByStatus(EStatusEmprestimo status);
        Task<int> GetTotal();
        Task<int> GetTotalOrderByStatus(EStatusEmprestimo status);
        Task<decimal> GetMoneyTotal();
        Task<decimal> GetTotalByThirtyToday(DateTime dataInicio, DateTime dataFim);
        Task<IEnumerable<LoanQuery>> GetLoanQuery(EStatusEmprestimo status);
    }
}
