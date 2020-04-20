﻿using Livecred.Domain.Enuns;
using Livecred.Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Livecred.Domain.Repositories
{
    public interface IRepositoryParcela
    {
        Task Create(Parcela parcela);
        Task Update(Parcela parcela);
        Task Delete(Guid id);
        Task<Parcela> GetById(Guid id);
        Task<Parcela> GetByStatus(EStatusParcela status);
        Task<Parcela> GetByPeriodo(DateTime periodo);
        Task<IEnumerable<Parcela>> GetAll();
    }
}
