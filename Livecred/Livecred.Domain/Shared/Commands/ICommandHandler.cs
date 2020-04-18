using System.Threading.Tasks;
namespace Livecred.Domain.Shared.Commands
{
    public interface ICommandHandler<T> where T : IcommandInput
    {
        Task<ICommandOutput> Handler(T command);
    }
}
