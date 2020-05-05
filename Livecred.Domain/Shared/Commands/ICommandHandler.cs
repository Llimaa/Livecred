using System.Threading.Tasks;
namespace Livecred.Domain.Shared.Commands
{
    public interface ICommandHandler<T> where T : Icommand
    {
        Task<ICommandOutput> Handler(T command);
    }
}
