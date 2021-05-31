using MediatR;
using System.Threading.Tasks;

namespace Lib.Application
{
    public interface ICommandMediator
    {
        Task<TCommandResponse> SendCommand<TCommandResponse>(IRequest<TCommandResponse> command)
            where TCommandResponse : ResponseBase;
    }
}
