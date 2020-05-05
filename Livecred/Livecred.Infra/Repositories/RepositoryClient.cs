using Dapper;
using Livecred.Domain.Models;
using Livecred.Domain.Repositories;
using Livecred.Domain.ValueObjects;
using Livecred.Infra.Context.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Livecred.Infra.Repositories
{
    public class RepositoryClient : IRepositoryClient
    {
        private readonly IDB _dB;

        public RepositoryClient(IDB dB)
        {
            _dB = dB;
        }

        public async Task Create(Client client)
        {
            using (var db = await _dB.GetConAsync())
            {
                string query = "   INSERT INTO [dbo].[Client]  " +
                               "          ([Id]		    	   " +
                               "           ,[Name]			   " +
                               "           ,[CPF]			   " +
                               "           ,[Telephone]	       " +
                               "           ,[Address])		   " +
                               "     VALUES				       " +
                               "            (@Id			   " +
                               "           ,@Name			   " +
                               "           ,@CPF			   " +
                               "           ,@Telephone		   " +
                               "           ,@Address)		   ";

                await db.ExecuteAsync(query, new
                {
                    Id = client.Id,
                    Name = client.Name.FirstName + " " + client.Name.LastName,
                    CPF = client.CPF.Number,
                    Telephone = client.Telephone,
                    Address = client.Address
                });
            }
        }

        public async Task Update(Client client)
        {
            using (var db = await _dB.GetConAsync())
            {
                var query = "	UPDATE [dbo].[Client]			" +
                            "	   SET [Id] = @Id				" +
                            "	      ,[Name] = @Name			" +
                            "	      ,[CPF] = @CPF				" +
                            "	      ,[Telephone] = @Telephone	" +
                            "	      ,[Address] = @Address		" +
                            "	 WHERE Id = @Id					";

                await db.ExecuteAsync(query, new
                {
                    Id = client.Id,
                    Name = client.Name.FirstName,
                    CPF = client.CPF.Number,
                    Telephone = client.Telephone,
                    Address = client.Address
                });
            }
        }

        public async Task Delete(Guid Id)
        {
            using (var db = await _dB.GetConAsync())
            {
                var query = "DELETE FROM [dbo].[Client] WHERE Id = @Id";
                await db.ExecuteAsync(query, new { Id = Id });
            }
        }

        public async Task<IEnumerable<Client>> GetAll()
        {
            using (var db = await _dB.GetConAsync())
            {
                var query = "SELECT [Id], [Telephone], [Address], [Name], [CPF] FROM [dbo].[Client];";
                return (await db.QueryAsync<Client, string, string, Client>(query, map: (client, name, document) =>
                {
                    client.SetName(name);
                    client.SetDocument(document);
                    return client;
                }, splitOn: "Name, CPF"));
            }
        }

        public async Task<Client> GetById(Guid id)
        {
            using (var db = await _dB.GetConAsync())
            {
                var query = "SELECT [Telephone],[Address],[Id],[Name],[CPF] FROM [dbo].[Client] WHERE Id = @Id";
                var _client = await db.QueryAsync<Client, string, string, Client>(query, param: new { Id = id }, map: (client, name, document) =>
                {
                    client.SetName(name);
                    client.SetDocument(document);
                    return client;
                }, splitOn: "Name, CPF");

                query = "SELECT [Id], [IdClient], [Valor],[Juro], [Status], [DataCadastro], [DataUpdate]" +
                    " FROM [dbo].[Loan] WHERE [IdClient] = @IdClient";

                var loans = await db.QueryAsync<Loan>(query, new { IdClient = id });
                _client.FirstOrDefault().SetInstancias(loans.ToList());
                //_client.FirstOrDefault().AddRageLoan(new List<Loan>(loans.ToList()));

                return _client.FirstOrDefault();
            }
        }

        public async Task<bool> ValidDocument(string document)
        {
            using (var db = await _dB.GetConAsync())
            {
                var query = "SELECT COUNT(Id) FROM Client WHERE CPF=@CPF";
                return await db.QueryFirstAsync<bool>(query, new { CPF = document });
            }
        }
    }
}
