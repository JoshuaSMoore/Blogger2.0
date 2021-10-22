using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Blogger.Services;

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
  }

}