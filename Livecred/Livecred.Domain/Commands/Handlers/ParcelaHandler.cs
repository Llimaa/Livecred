using Livecred.Domain.Commands.Inputs.Parcela;
using Livecred.Domain.Commands.output;
using Livecred.Domain.Models;
using Livecred.Domain.Repositories;
using Livecred.Domain.Shared.Commands;
using System.Threading.Tasks;

namespace Livecred.Domain.Commands.Handlers
{
    public class ParcelaHandler : ICommandHandler<ParcelaInput>,
                                  ICommandHandler<ParcelaUpdateValor>,
                                  ICommandHandler<ParcelaUpdateJuroAtraso>,
                                  ICommandHandler<ParcelaUpdateDataVencimento>
    {

        private readonly IRepositoryParcela _repositoryParcela;

        public ParcelaHandler(IRepositoryParcela repositoryParcela)
        {
            _repositoryParcela = repositoryParcela;
        }

        public async Task<ICommandOutput> Handler(ParcelaInput command)
        {
            command.Validate();
            if (command.Invalid)
                return new GenericCommandResult(false, "Dados inválidos", command.Notifications);

            var parcela = new Parcela(command.Valor, command.IdLoan);
            await _repositoryParcela.Create(parcela);

            return new GenericCommandResult(true, "parcela cadastrada com sucesso", parcela);
        }

        public async Task<ICommandOutput> Handler(ParcelaUpdateValor command)
        {
            command.Validate();
            if (command.Invalid)
                return new GenericCommandResult(false, "Dados incorretos", command.Notifications);

            var parcela = await _repositoryParcela.GetById(command.Id);

            parcela.UpdateValor(command.Valor);
            await _repositoryParcela.UpdateValor(command.Id, command.Valor);
            return new GenericCommandResult(true, "Valor Atualizado", parcela);

        }

        public async Task<ICommandOutput> Handler(ParcelaUpdateJuroAtraso command)
        {
            command.Validate();
            if (command.Invalid)
                return new GenericCommandResult(false, "Dados inválidos", command.Notifications);

            var parcela = await _repositoryParcela.GetById(command.Id);
            parcela.UpdateJuroAtraso(command.Juro);

            await _repositoryParcela.UpdateJuro(command.Id, command.Juro);
            return new GenericCommandResult(true, "Dados atualizaods", parcela);

        }

        public async Task<ICommandOutput> Handler(ParcelaUpdateDataVencimento command)
        {
            command.Validate();
            if (command.Invalid)
                return new GenericCommandResult(false, "Dados inválidos", command.Notifications);

            var parcela = await _repositoryParcela.GetById(command.Id);
            parcela.UpdateDataVencimento(command.DataVencimento);

            await _repositoryParcela.UpdateDataVencimento(command.Id, command.DataVencimento);
            return new GenericCommandResult(true, "Dados atualizaods", parcela);
        }
    }
}
