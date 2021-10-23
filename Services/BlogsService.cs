using System;
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
  public Blog GetBlogById(int blogId)
    {
      Blog foundBlog = _blogsRepository.GetBlogById(blogId);
      if(foundBlog == null)
      {
        throw new Exception("Nope, not a blog dude");
      }
      return foundBlog;
    }

    public Blog CreateBlog(Blog blogData)
    {
      return _blogsRepository.CreateBlog(blogData);
    }

    internal void DeleteBlog(int blogId, string userId)
    {
      Blog foundBlog = GetBlogById(blogId);
      if(foundBlog.CreatorId != userId)
      {
        throw new Exception("This aint your Blog bruh");
      }
      _blogsRepository.DeleteBlog(blogId);
    }
  }
}