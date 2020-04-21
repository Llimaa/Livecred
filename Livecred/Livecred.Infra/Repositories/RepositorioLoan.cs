using Dapper;
using Livecred.Domain.Enuns;
using Livecred.Domain.Models;
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
                            "           ,[DataCadastro]	  " +
                            "           ,[DataUpdate])	  " +
                            "     VALUES				  " +
                            "           (@Id			  " +
                            "           ,@IdClient		  " +
                            "           ,@Valor			  " +
                            "           ,@Juro			  " +
                            "           ,@Status		  " +
                            "           ,@DataCadastro	  " +
                            "           ,@DataUpdate)	  ";

                await db.ExecuteAsync(query, new
                {
                    Id = loan.Id,
                    IdClient = loan.IdClient,
                    Valor = loan.Valor,
                    Juro = loan.Juro,
                    Status = loan.Status,
                    DataCadastro = loan.DataCadastro,
                    DataUpdate = loan.DataUpdate
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
                            "  WHERE Status = @Status	" +
                            "	  FROM [dbo].[Loan]		";
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
    }
}
