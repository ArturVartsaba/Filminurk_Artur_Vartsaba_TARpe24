using Filminurk.Core.Domain;
using Filminurk.Core.Dto;

namespace Filminurk.Core.ServiceInterface
{
    public interface IActorServices
    {
        Task<Actor> Create(ActorDTO dto);
    }
}
