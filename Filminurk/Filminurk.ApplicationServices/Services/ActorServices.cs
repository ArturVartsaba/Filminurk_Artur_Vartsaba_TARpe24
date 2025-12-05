using Filminurk.Core.Domain;
using Filminurk.Core.Dto;
using Filminurk.Core.ServiceInterface;
using Filminurk.Data;

namespace Filminurk.ApplicationServices.Services
{
    public class ActorServices : IActorServices
    {
        private readonly FilminurkTARpe24Context _context;
        public ActorServices(FilminurkTARpe24Context context)
        {
            _context = context;
        }
        public async Task<Actor> Create(ActorDTO dto)
        {
            Actor actor = new Actor();
            actor.ActorID = Guid.NewGuid();
            actor.FirstName = dto.FirstName;
            actor.LastName = dto.LastName;
            actor.NickName = dto.NickName;
            actor.MoviesActedFor = dto.MoviesActedFor;
            actor.PortraitID = dto.PortraitID;
            actor.Age = (int)dto.Age;
            actor.CountryOfOrigin = dto.CountryOfOrigin;
            actor.Gender = (Core.Domain.Gender)dto.Gender;

            await _context.Actors.AddAsync(actor);
            await _context.SaveChangesAsync();

            return actor;
        }
    }
}
