using Dapper;
using Livecred.Domain.Enuns;
using Livecred.Domain.Models;
using Livecred.Domain.Queries;
using Livecred.Domain.Repositories;
using Livecred.Infra.Context.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Livecred.Infra.Repositories
{
    public class RepositorioLoan : IRepositoryLoan
    {
        private readonly IDB _dB;

        public RepositorioLoan(IDB dB)
        {
            _dB = dB;
        }

        public async Task Create(Loan loan)
        {
            using (var db = await _dB.GetConAsync())
            {
                var query = "	INSERT INTO [dbo].[Loan]  " +
                            "           ([Id]			  " +
                            "           ,[IdClient]		  " +
                            "           ,[Valor]		  " +
                            "           ,[Juro]			  " +
                            "           ,[Status]		  " +
                            "           ,[DataCadastro])  " +
                            "     VALUES				  " +
                            "           (@Id			  " +
                            "           ,@IdClient		  " +
                            "           ,@Valor			  " +
                            "           ,@Juro			  " +
                            "           ,@Status		  " +
                            "           ,@DataCadastro)	  ";

                await db.ExecuteAsync(query, new
                {
                    Id = loan.Id,
                    IdClient = loan.IdClient,
                    Valor = loan.Valor,
                    Juro = loan.Juro,
                    Status = 1,
                    DataCadastro = loan.DataCadastro
                });
            }
        }

        public async Task Update(Loan loan)
        {
            using (var db = await _dB.GetConAsync())
            {
                var query = "	UPDATE [dbo].[Loan]						" +
                            "	  SET [Id] = @Id						" +
                            "	     ,[Valor] = @Valor					" +
                            "	     ,[Juro] = @Juro					" +
                            "	     ,[Status] = @Status				" +
                            "	     ,[DataCadastro] = @DataCadastro	" +
                            "	     ,[DataUpdate] = @DataUpdate		" +
                            "	WHERE Id = @Id							";
                await db.ExecuteAsync(query, new
                {
                    Id = loan.Id,
                    Valor = loan.Valor,
                    Juro = loan.Juro,
                    Status = loan.Status,
                    DataCadastro = loan.DataCadastro,
                    DataUpdate = loan.DataUpdate
                });
            }
        }

        public async Task Delete(Guid id)
        {
            using (var db = await _dB.GetConAsync())
            {
                var query = "DELETE FROM [dbo].[Loan] WHERE Id = @Id";
                await db.ExecuteAsync(query, new { Id = id });
            }
        }

        public async Task<IEnumerable<Loan>> GetAll()
        {
            using (var db = await _dB.GetConAsync())
            {
                var query = "	SELECT [Id]				" +
                            "	      ,[IdClient]		" +
                            "	      ,[Valor]			" +
                            "	      ,[Juro]			" +
                            "	      ,[Status]			" +
                            "	      ,[DataCadastro]	" +
                            "	      ,[DataUpdate]		" +
                            "	  FROM [dbo].[Loan]		";
                return await db.QueryAsync<Loan>(query);
            }
        }

        public async Task<IEnumerable<Loan>> GetAllOrderByStatus(EStatusEmprestimo status)
        {
            using (var db = await _dB.GetConAsync())
            {
                var query = "	SELECT [Id]				" +
                            "	      ,[IdClient]		" +
                            "	      ,[Valor]			" +
                            "	      ,[Juro]			" +
                            "	      ,[Status]			" +
                            "	      ,[DataCadastro]	" +
                            "	      ,[DataUpdate]		" +
                            "	  FROM [dbo].[Loan]		" +
                              "  WHERE Status = @Status	";
                return await db.QueryAsync<Loan>(query, new { Status = status });
            }
        }

        public async Task<Loan> GetById(Guid id)
        {
            using (var db = await _dB.GetConAsync())
            {
                var query = "	SELECT [Id]				" +
                            "	      ,[IdClient]		" +
                            "	      ,[Valor]			" +
                            "	      ,[Juro]			" +
                            "	      ,[Status]			" +
                            "	      ,[DataCadastro]	" +
                            "	      ,[DataUpdate]		" +
                            "	  FROM [dbo].[Loan]		" +
                            "     WHERE Id = @Id	    ";

                var loan = await db.QueryFirstOrDefaultAsync<Loan>(query, new { Id = id });

                query = "	SELECT [Id]                 " +
                        "	      ,[IdLoan]				" +
                        "	      ,[Valor]				" +
                        "	      ,[JuroAtraso]			" +
                        "	      ,[Status]				" +
                        "	      ,[DataVencimento]		" +
                        "	  FROM [dbo].[Parcela] 		" +
                        "	  WHERE IdLoan = @IdLoan	";

                var parcelas = await db.QueryAsync<Parcela>(query, new { IdLoan = id });
                loan.AddRangeParcela(parcelas.ToList());
                return loan;
            }
        }

        public async Task<IEnumerable<Loan>> GetAllbyIdClient(Guid IdClient)
        {
            using (var db = await _dB.GetConAsync())
            {
                var query = "	SELECT [Id]				" +
                            "	      ,[IdClient]		" +
                            "	      ,[Valor]			" +
                            "	      ,[Juro]			" +
                            "	      ,[Status]			" +
                            "	      ,[DataCadastro]	" +
                            "	      ,[DataUpdate]		" +
                            "	  FROM [dbo].[Loan]		" +
                            "WHERE [IdClient] = @IdClient	";

                return await db.QueryAsync<Loan>(query, new { IdClient = IdClient });
            }
        }

        public async Task<int> GetTotal()
        {
            using (var db = await _dB.GetConAsync())
            {
                var query = "SELECT COUNT(Id) FROM [dbo].[Loan]";

                return await db.QueryFirstOrDefaultAsync<int>(query);
            }
        }

        public async Task<int> GetTotalOrderByStatus(EStatusEmprestimo status)
        {
            using (var db = await _dB.GetConAsync())
            {
                var query = "SELECT COUNT(Id) FROM [dbo].[Loan]  WHERE Status =@Status";

                return await db.QueryFirstOrDefaultAsync<int>(query, new { Status = status });
            }
        }

        public async Task<decimal> GetMoneyTotal()
        {
            using (var db = await _dB.GetConAsync())
            {
                var query = "SELECT ISNULL(SUM(Valor),0)+ISNULL(SUM(Juro),0) FROM [dbo].[Loan]";

                return await db.QueryFirstOrDefaultAsync<decimal>(query);
            }
        }

        public async Task<decimal> GetTotalByThirtyToday(DateTime dataInicio, DateTime dataFim)
        {
            using (var db = await _dB.GetConAsync())
            {
                var query = "SELECT ISNULL(SUM(Valor),0) FROM [dbo].[Loan] where DataCadastro between @DataInicio and @DataFim;";

                return await db.QueryFirstOrDefaultAsync<decimal>(query, new { DataInicio = dataInicio, DataFim = dataFim });
            }
        }

        public async Task<IEnumerable<LoanQuery>> GetLoanQuery(EStatusEmprestimo status)
        {
            using (var db = await _dB.GetConAsync())
            {
                var query = "	SELECT L.[Id]				                 " +
                            "		,C.[Name] AS NameClient					 " +
                            "	    ,L.[Valor]								 " +
                            "	    ,L.[Juro]								 " +
                            "	    ,L.[Status]								 " +
                            "	    ,L.[DataCadastro] 						 " +
                            "	  FROM [dbo].[Loan] AS L					 " +
                            "	  INNER JOIN Client AS C ON C.Id = L.IdClient" +
                            "	    WHERE L.Status = @Status				 " +
                            "	  ORDER BY DataCadastro DESC;                ";

                var loanQueries = await db.QueryAsync<LoanQuery>(query, new { Status = status });

                foreach (var item in loanQueries)
                {
                    query = "SELECT COUNT(Id) FROM Parcela WHERE IdLoan =@IdLoan";
                    item.TotalParcelas = await db.QueryFirstOrDefaultAsync<int>(query, new { IdLoan = item.Id });

                    query = "SELECT COUNT(Id) FROM Parcela WHERE Status = 2 AND IdLoan = @IdLoan";
                    item.TotalParcelasPagas = await db.QueryFirstOrDefaultAsync<int>(query, new { IdLoan = item.Id });
                }

                return loanQueries;
            }
        }
    }
}
