using Livecred.Domain.Shared.Commands;
namespace Livecred.Domain.Commands.output
{
    public class GenericCommandResult : ICommandOutput
    {
        public GenericCommandResult(bool success, string message, object data)
        {
            Success = success;
            Message = message;
            Data = data;
        }

        public GenericCommandResult() { }

        public bool Success { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
    }
}
