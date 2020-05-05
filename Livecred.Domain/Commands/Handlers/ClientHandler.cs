using Livecred.Domain.Commands.Inputs.Client;
using Livecred.Domain.Commands.output;
using Livecred.Domain.Models;
using Livecred.Domain.Repositories;
using Livecred.Domain.Shared.Commands;
using Livecred.Domain.ValueObjects;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Livecred.Domain.Commands.Handlers
{
    public class ClientHandler : ICommandHandler<ClientInsert>, ICommandHandler<ClientUpdate>
    {
        private readonly IRepositoryClient _repositoryClient;

        public ClientHandler(IRepositoryClient repositoryClient)
        {
            this._repositoryClient = repositoryClient;
        }

        public async Task<ICommandOutput> Handler(ClientInsert command)
        {
            command.Validate();
            if (command.Invalid)
                return new GenericCommandResult(false, "Dados inválidos", command.Notifications);

            var name = new Name(command.FirstName, command.LastName);
            var document = new Document(command.NuberDocument);

            if (name.Invalid)
                new GenericCommandResult(false, "Dados do name inválidos", name.Notifications);

            if (document.Invalid)
                new GenericCommandResult(false, "Dados do documento inválidos", document.Notifications);

            var client = new Client(name, document, command.Telephone, command.Address, new List<Loan>());

            await _repositoryClient.Create(client);
            return new GenericCommandResult(true, "Cliente cadastrado com sucesso", client);
        }

        public async Task<ICommandOutput> Handler(ClientUpdate command)
        {
            command.Validate();
            if (command.Invalid)
                return new GenericCommandResult(false, "Dados Inválidos", command.Notifications);

            var name = new Name(command.FirstName, command.LastName);
            var document = new Document(command.NuberDocument);

            if (name.Invalid)
                new GenericCommandResult(false, "Dados do name inválidos", name.Notifications);

            if (document.Invalid)
                new GenericCommandResult(false, "Dados do documento inválidos", document.Notifications);

            var client = await _repositoryClient.GetById(command.Id);
            client.Update(command.FirstName, command.LastName, command.NuberDocument, command.Telephone, command.Address);
            await _repositoryClient.Update(client);
            return new GenericCommandResult(true, "Dados do cliente atualizados com sucesso", client);
        }
    }
}
