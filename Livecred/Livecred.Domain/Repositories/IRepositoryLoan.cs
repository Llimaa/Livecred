using Livecred.Domain.Enuns;
using Livecred.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Livecred.Domain.Repositories
{
    public interface IRepositoryLoan
    {
        void Create(Loan loan);
        void Update(Loan loan);
        void Delete(Guid guid);
        Loan GetById(Guid id);
        IEnumerable<Loan> GetAll();
        IEnumerable<Loan> GetAllOrderByStatus(EStatusEmprestimo status);
    }
}
