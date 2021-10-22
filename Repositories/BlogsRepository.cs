using System.Collections.Generic;
using System.Data;
using System.Linq;
using Blogger.Models;
using Dapper;

namespace Blogger.Repositories
{
  public class BlogsRepository
  {
    private readonly IDbConnection _db;

    public BlogsRepository(IDbConnection db)
    {
      _db = db;
    }
  
  public List<Blog> Get()
  {
    string sql = "SELECT * FROM blogs";
    return _db.Query<Blog>(sql).ToList();
  }

    public Blog GetBlogById(int blogId)
    {
      string sql = @"
      SELECT
      b.*,
      a.*
      FROM blogs b
      JOIN accounts a on b.creatorId = a.id
      WHERE b.id = @blogId;
      ";
      return _db.Query<Blog, Account, Blog>(sql, (b, a) =>
    {
      b.Creator = a;
      return b;
    }, new{blogId}).FirstOrDefault(); 
    }
    public Blog CreateBlog(Blog blogData)
    {
      string sql = @"
      INSERT INTO blogs(
        creatorId,
        title,
        body,
        imgUrl,
        published
        )
        VALUES (
        @CreatorId,  
        @Title,
        @Body,
        @ImgUrl,
        @Published
        );
        SELECT LAST_INSERT_ID();";
        int id = _db.ExecuteScalar<int>(sql, blogData);
        blogData.Id = id;
        return blogData;
    }
  }
}