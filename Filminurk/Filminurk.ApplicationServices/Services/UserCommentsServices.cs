using Filminurk.Core.Domain;
using Filminurk.Core.Dto;
using Filminurk.Core.ServiceInterface;
using Filminurk.Data;
using Microsoft.EntityFrameworkCore;

namespace Filminurk.ApplicationServices.Services
{
    public class UserCommentsServices : IUserCommentsServices
    {
        private readonly FilminurkTARpe24Context _context;
        public UserCommentsServices(FilminurkTARpe24Context context)
        {
            _context = context;
        }

        public async Task<UserComment> NewComment(UserCommentDTO newcommentDTO)
        {
            UserComment domain = new UserComment();

            domain.CommentID = Guid.NewGuid();
            domain.CommentBody = newcommentDTO.CommentBody;
            domain.CommenterUserID = newcommentDTO.CommenterUserID;
            domain.CommentedScore = newcommentDTO.CommentedScore;
            domain.CommentCreatedAt = DateTime.Now;
            domain.CommentModifiedAt = DateTime.Now;
            domain.IsHelpful = (int)newcommentDTO.IsHelpful;
            domain.IsHarmful = (int)newcommentDTO.IsHarmful;

            await _context.UserComments.AddAsync(domain);
            await _context.SaveChangesAsync();

            return domain;
        }

        public async Task<UserComment> DetailAsync(Guid id)
        {
            var returnComment = await _context.UserComments
                .FirstOrDefaultAsync(x => x.CommentID == id);
            return returnComment;
        }
        public async Task<UserComment> Delete(Guid id) 
        { 
            var result = await _context.UserComments
                .FirstOrDefaultAsync(x => x.CommentID == id);
            if (result != null)
            {
                _context.UserComments.Remove(result);
                await _context.SaveChangesAsync();
            }
            return result;
            //TODO: Send email to user, that comment was removed, containing original comment.
        }
    }
}
