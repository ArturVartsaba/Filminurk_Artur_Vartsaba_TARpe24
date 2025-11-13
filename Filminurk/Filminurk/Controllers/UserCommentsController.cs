using AspNetCoreGeneratedDocument;
using Filminurk.Core.Dto;
using Filminurk.Core.ServiceInterface;
using Filminurk.Data;
using Filminurk.Models.UserComments;
using Microsoft.AspNetCore.Mvc;
using Microsoft.JSInterop.Infrastructure;

namespace Filminurk.Controllers
{
    public class UserCommentsController : Controller
    {
        private readonly FilminurkTARpe24Context _context;
        private readonly IUserCommentsServices _userCommentsServices;
        public UserCommentsController
            (
            FilminurkTARpe24Context context,
            IUserCommentsServices userCommentServices
            )
        {
            _context = context;
            _userCommentsServices = userCommentServices;
        }
        public IActionResult Index()
        {
            var result = _context.UserComments
                .Select(c => new UserCommentsIndexViewModel
                {
                    CommentID = c.CommentID,
                    CommentBody = c.CommentBody,
                    IsHarmful = (int)c.IsHarmful,
                    CommentCreatedAt = c.CommentCreatedAt,
                }
            );
            return View(result);
        }
        [HttpGet]
        public IActionResult NewComment()
        {
            //TODO: erista kas tegemist on admini või tavalise kasutajaga
            UserCommentsCreateViewModel newcomment = new();
            return View(newcomment);
        }
        [HttpPost, ActionName("NewComment")]
        //meetodile ei tohi panna allowanonymous
        public async Task<IActionResult> NewCommentPost(UserCommentsCreateViewModel newcommentVM)
        {
            // check dto
            newcommentVM.CommenterUserID = "00000000-0000-0000-000000000001";
            //TODO: newcommenti manuaalne seadmine, asenda pärast kasutaja ID-ga
            Console.WriteLine(newcommentVM.CommenterUserID);
            if (ModelState.IsValid)
            {
                var dto = new UserCommentDTO() { };
                dto.CommentID = newcommentVM.CommentID;
                dto.CommentBody = newcommentVM.CommentBody;
                dto.CommenterUserID = newcommentVM.CommenterUserID;
                dto.CommentedScore = newcommentVM.CommentedScore;
                dto.CommentCreatedAt = newcommentVM.CommentCreatedAt;
                dto.CommentModifiedAt = newcommentVM.CommentModifiedAt;
                dto.CommentDeletedAt = newcommentVM.CommentDeletedAt;
                dto.IsHelpful = newcommentVM.IsHelpful;
                dto.IsHarmful = newcommentVM.IsHarmful;

                var result = await _userCommentsServices.NewComment(dto);
                if (result == null)
                {
                    return NotFound();
                }
                //TODO: erista ära kas tegu admini või kasutajaga, admin
                //tagastub admin-comments-index, kasutaja aga vastava filmi juurde
                return RedirectToAction(nameof(Index));
                //return RedirectToAction("Details", "Movies", id)
            }
            return NotFound();
        }
        [HttpGet]
        public async Task<IActionResult> DetailsAdmin(Guid id)
        {
            var requestedComment = await _userCommentsServices.DetailAsync(id);

            if (requestedComment == null) { return NotFound(); }

            var commentVM = new UserCommentsIndexViewModel();

            commentVM.CommentID = requestedComment.CommentID;
            commentVM.CommentBody = requestedComment.CommentBody;
            commentVM.CommenterUserID = requestedComment.CommenterUserID;
            commentVM.CommentedScore = requestedComment.CommentedScore;
            commentVM.CommentCreatedAt = requestedComment.CommentCreatedAt;
            commentVM.CommentModifiedAt = requestedComment.CommentModifiedAt;
            commentVM.CommentDeletedAt = requestedComment.CommentDeletedAt;

            return View(commentVM);

        }
        [HttpGet]
        public async Task<IActionResult> DeleteComment(Guid id) 
        {
            var deleteEntry = await _userCommentsServices.DetailAsync(id);

            if (deleteEntry == null) { return NotFound(); }

            var commentVM = new UserCommentsIndexViewModel();
            commentVM.CommentID = deleteEntry.CommentID;
            commentVM.CommentBody = deleteEntry.CommentBody;
            commentVM.CommenterUserID = deleteEntry.CommenterUserID;
            commentVM.CommentedScore = deleteEntry.CommentedScore;
            commentVM.CommentCreatedAt = deleteEntry.CommentCreatedAt;
            commentVM.CommentModifiedAt = deleteEntry.CommentModifiedAt;
            commentVM.CommentDeletedAt = deleteEntry.CommentDeletedAt;
            return View("DeleteAdmin", commentVM);
        }
        [HttpPost]
        public async Task<IActionResult> DeleteAdminPost(Guid id) 
        { 
            var deleteThisComment = await _userCommentsServices.Delete(id);
            if (deleteThisComment == null) { return NotFound(); }
            return RedirectToAction("Index");
        }
    }
}