using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Blogger.Services;
using Microsoft.AspNetCore.Authorization;
using Blogger.Models;
using CodeWorks.Auth0Provider;
using System.Threading.Tasks;

namespace Blogger.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class BlogsController : ControllerBase
  {
    private readonly BlogsService _blogsService;

    public BlogsController(BlogsService blogsService)
    {
      _blogsService = blogsService;
    }
    [HttpGet]

    public ActionResult<List<Blog>> Get()
    {
      try
      {
           return Ok(_blogsService.Get());
      }
      catch (System.Exception e)
      {
          return BadRequest(e.Message);
      }
    }
    [HttpGet("{blogId}")]

    public ActionResult<Blog> GetBlogById(int blogId)
    {
      try
      {
           return Ok(_blogsService.GetBlogById(blogId));
      }
      catch (System.Exception e)
      {
      return BadRequest(e.Message);
      }
    }
    [Authorize]
    [HttpPost]
    public async Task<ActionResult<Blog>> CreateBlog([FromBody] Blog blogData)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
           blogData.CreatorId = userInfo.Id;
           Blog createdBlog = _blogsService.CreateBlog(blogData);
           createdBlog.Creator = userInfo;
           return createdBlog;
      }
      catch (System.Exception e)
      {
       return BadRequest(e.Message);
      }
    }
     [Authorize]
     [HttpDelete("{blogId}")]

     public async Task<ActionResult<string>> DeleteBlog(int blogId)
     {
       try
       {
            Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
            _blogsService.DeleteBlog(blogId, userInfo.Id);
            return Ok("Blog has been deleted");
       }
       catch (System.Exception e)
       {
         return BadRequest(e.Message);
       }
     }
  }

}