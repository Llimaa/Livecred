using Livecred.Domain.Commands.Inputs.Loan;
using Livecred.Domain.Commands.output;
using Livecred.Domain.Models;
using Livecred.Domain.Repositories;
using Livecred.Domain.Shared.Commands;
using System.Threading.Tasks;

namespace Livecred.Domain.Commands.Handlers
{
    public class LoanHandler : ICommandHandler<LoanInsert>, ICommandHandler<LoanUpdate>
    {
        private readonly IRepositoryLoan _repositoryLoan;

        public LoanHandler(IRepositoryLoan repositoryLoan)
        {
            _repositoryLoan = repositoryLoan;
        }

        public async Task<ICommandOutput> Handler(LoanInsert command)
        {
            command.Validate();
            if (command.Invalid)
                return new GenericCommandResult(false, "Dados incorretos ", command.Notifications);

            var loan = new Loan(command.Valor, command.Juro);
            await _repositoryLoan.Create(loan);

            return new GenericCommandResult(true, "Emprestimo realizado com sucesso", loan);
        }

        public async Task<ICommandOutput> Handler(LoanUpdate command)
        {
            command.Validate();
            if (command.Invalid)
                return new GenericCommandResult(false, "Dados incoretos", command.Notifications);

            var loan = await _repositoryLoan.GetById(command.Id);
            loan.Update(command.Valor, command.Juro, command.Status);

            await _repositoryLoan.Update(loan);
            return new GenericCommandResult(true, "Dados atualizados", loan);
        }
    }
}
