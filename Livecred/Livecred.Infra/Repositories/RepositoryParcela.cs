using Dapper;
using Livecred.Domain.Enuns;
using Livecred.Domain.Models;
using Livecred.Domain.Repositories;
using Livecred.Infra.Context.Core;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Livecred.Infra.Repositories
{
    public class RepositoryParcela : IRepositoryParcela
    {
        private readonly IDB _dB;

        public RepositoryParcela(IDB dB)
        {
            _dB = dB;
        }

        public async Task Create(Parcela parcela)
        {
            using (var db = await _dB.GetConAsync())
            {
                var query = "	INSERT INTO [dbo].[Parcela] " +
                            "	       ([Id]				" +
                            "	       ,[IdLoan]			" +
                            "	       ,[Valor]				" +
                            "	       ,[JuroAtraso]		" +
                            "	       ,[Status]			" +
                            "	       ,[DataVencimento])	" +
                            "	 VALUES						" +
                            "	       (@Id					" +
                            "	       ,@IdLoan				" +
                            "	       ,@Valor				" +
                            "	       ,@JuroAtraso			" +
                            "	       ,@Status				" +
                            "	       ,@DataVencimento)	";
                await db.ExecuteAsync(query, new
                {
                    Id = parcela.Id,
                    IdLoan = parcela.IdLoan,
                    Valor = parcela.Valor,
                    JuroAtraso = parcela.JuroAtraso,
                    Status = parcela.Status,
                    DataVencimento = parcela.DataVencimento
                });
            }
        }

        public async Task UpdateDataVencimento(Guid id, DateTime dataVencimento)
        {
            using (var db = await _dB.GetConAsync())
            {
                var query = "	UPDATE [dbo].[Parcela] SET          " +
                            "   [DataVencimento] = @DataVencimento  " +
                            "	WHERE Id = @Id				        ";

                await db.ExecuteAsync(query, new
                {
                    Id = id,
                    DataVencimento = dataVencimento
                });
            }
        }

        public async Task UpdateJuro(Guid id, double juro)
        {
            using (var db = await _dB.GetConAsync())
            {
                var query = "	UPDATE [dbo].[Parcela] SET          " +
                            "   [Juro] = @Juro  " +
                            "	WHERE Id = @Id				        ";

                await db.ExecuteAsync(query, new
                {
                    Id = id,
                    Juro = juro
                });
            }
        }

        public async Task UpdateValor(Guid id, decimal valor)
        {
            using (var db = await _dB.GetConAsync())
            {
                var query = "	UPDATE [dbo].[Parcela] SET          " +
                            "   [Valor] = @Valor  " +
                            "	WHERE Id = @Id				        ";

                await db.ExecuteAsync(query, new
                {
                    Id = id,
                    Valor = valor
                });
            }
        }

        public async Task Delete(Guid id)
        {
            using (var db = await _dB.GetConAsync())
            {
                var query = "DELETE FROM [dbo].[Parcela] WHERE Id = @Id";
                await db.ExecuteAsync(query, new { Id = id });
            }
        }

        public async Task<IEnumerable<Parcela>> GetAll()
        {
            using (var db = await _dB.GetConAsync())
            {
                var query = "	SELECT [Id]				" +
                            "	      ,[IdLoan]			" +
                            "	      ,[Valor]			" +
                            "	      ,[JuroAtraso]		" +
                            "	      ,[Status]			" +
                            "	      ,[DataVencimento]	" +
                            "	  FROM [dbo].[Parcela]	";
                return await db.QueryAsync<Parcela>(query);
            }
        }

        public async Task<Parcela> GetById(Guid id)
        {
            using (var db = await _dB.GetConAsync())
            {
                var query = "	SELECT [Id]				" +
                            "	      ,[IdLoan]			" +
                            "	      ,[Valor]			" +
                            "	      ,[JuroAtraso]		" +
                            "	      ,[Status]			" +
                            "	      ,[DataVencimento]	" +
                            "	  FROM [dbo].[Parcela]	" +
                            "	  WHERE Id = @Id	    ";
                return await db.QueryFirstOrDefaultAsync<Parcela>(query, new { Id = id });
            }
        }

        public async Task<Parcela> GetByPeriodo(DateTime periodo)
        {
            using (var db = await _dB.GetConAsync())
            {
                var query = "	SELECT [Id]				" +
                            "	      ,[IdLoan]			" +
                            "	      ,[Valor]			" +
                            "	      ,[JuroAtraso]		" +
                            "	      ,[Status]			" +
                            "	      ,[DataVencimento]	" +
                            "	  FROM [dbo].[Parcela]	" +
                            "	  WHERE DataVencimento = @Id BETWEEN @DataVencimento AND @DataAtual	    ";

                return await db.QueryFirstOrDefaultAsync<Parcela>(query, new { DataVencimento = periodo, DataAtual = DateTime.Now });
            }
        }

        public async Task<Parcela> GetByStatus(EStatusParcela status)
        {
            using (var db = await _dB.GetConAsync())
            {
                var query = "	SELECT [Id]				" +
                            "	      ,[IdLoan]			" +
                            "	      ,[Valor]			" +
                            "	      ,[JuroAtraso]		" +
                            "	      ,[Status]			" +
                            "	      ,[DataVencimento]	" +
                            "	  FROM [dbo].[Parcela]	" +
                            "	WHERE Status = @Status	";
                return await db.QueryFirstOrDefaultAsync<Parcela>(query, new { Status = status });
            }
        }
    }
}
