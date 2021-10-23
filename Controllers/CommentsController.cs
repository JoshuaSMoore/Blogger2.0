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
         commentData.Creator = userInfo;
         Comment createdComment = _commentsService.CreateComment(commentData);
         return createdComment;
    }
    catch (System.Exception e)
    {
      return BadRequest(e.Message);
    }
  }

 [HttpGet("{commentId}")]
    public ActionResult<Comment> GetById(int commentId)
    {
      try
      {
          return Ok(_commentsService.GetById(commentId));
      }
      catch (System.Exception e)
      {
        return BadRequest(e.Message);
      }
    }

  [Authorize]
  [HttpPut("{commentId}")]

  public async Task<ActionResult<Comment>> EditComment(int commentId, [FromBody] Comment commentData)
  {
    try
    {
         Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
         var comment = _commentsService.EditComment(commentId, commentData);
         return Ok(comment);
    }
    catch (System.Exception e)
    {
      return BadRequest(e.Message);
    }
  }
  [Authorize]
  [HttpDelete("{commentId}")]

  public async Task<ActionResult<Comment>> RemoveComment(int id)
  {
    try
    {
         Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
         var comment = _commentsService.RemoveComment(id, userInfo.Id);
         comment.CreatorId = userInfo.Id;
         return Ok("Comment is now gone");
    }
    catch (System.Exception e)
    {
      return BadRequest(e.Message);
    }
  }
  }
}