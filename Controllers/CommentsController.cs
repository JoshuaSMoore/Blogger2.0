using Microsoft.AspNetCore.Mvc;
using Blogger.Services;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using Blogger.Models;
using CodeWorks.Auth0Provider;

namespace Blogger.Controller
{
  [ApiController]
  [Route("api/[controller]")]
  public class CommentsController : ControllerBase
  {
    private readonly CommentsService _commentsService;

  public CommentsController(CommentsService commentsService)
  {
    _commentsService = commentsService;
  }
  [Authorize]
  [HttpPost]

  public async Task<ActionResult<Comment>> CreateComment([FromBody] Comment commentData)
  {
    try
    {
         Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
         commentData.CreatorId = userInfo.Id;
         Comment createdComment = _commentsService.CreateComment(commentData);
         createdComment.Creator = userInfo;
         return createdComment;
    }
    catch (System.Exception e)
    {
      return BadRequest(e.Message);
    }
  }
  }
}