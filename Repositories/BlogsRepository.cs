using System.Collections.Generic;
using System.Data;
using System.Linq;
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
      return _db
      .QueryFirstOrDefault<Blog>("SELECT * FROM blogs WHERE id = @blogId", new { blogId });  
    }
    public Blog CreateBlog(Blog blogData)
    {
      string sql = @"
      INSERT INTO blogs(creatorId,
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