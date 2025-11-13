using Filminurk.Core.Domain;
using Filminurk.Core.Dto;

namespace Filminurk.Core.ServiceInterface
{
    public interface IUserCommentsServices
    {
        Task<UserComment> NewComment(UserCommentDTO newcommentDTO);
    }
}
