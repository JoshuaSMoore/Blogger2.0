using System.Collections.Generic;
using Blogger.Repositories;

namespace Blogger.Services
{
  public class BlogsService
  {
    private readonly BlogsRepository _blogsRepository;

    public BlogsService(BlogsRepository blogsRepository)
  {
    _blogsRepository = blogsRepository;
  }

  public List<Blog> Get()
  {
    return _blogsRepository.Get();
  }
  }
}