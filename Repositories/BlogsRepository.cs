using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Blogger.Models;
using Dapper;
s
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

    public void DeleteBlog(int blogId)
    {
      string sql = "DELETE FROM blogs WHERE id = @blogId LIMIT 1;";
      var affectedRows = _db.Execute(sql, new {blogId});
      if(affectedRows == 0)
      {
        throw new Exception("Cannot Delete a Blog that is already delorted");
      }
    }

   internal List<Blog> GetBlogsByAccount(string userId)
    {
      string sql = "SELECT * FROM blogs b WHERE b.creatorId = @userId";
      return _db.Query<Blog>(sql, new{userId}).ToList();
    }

    public Blog UpdateBlog(int blogId, Blog blogData)
    {
      blogData.Id = blogId;
      var sql = @"
      UPDATE blogs
      SET
      title = @Title,
      body = @Body,
      imgUrl = @ImgUrl,
      published = @Published
      WHERE id = @Id";
      var affectedRows = _db.Execute(sql, blogData);

      if (affectedRows > 1)
      {
        throw new System.Exception("What the heck are you doing?");
      }
      if (affectedRows == 0)
      {
        throw new System.Exception("Update did not work");
      }
      return blogData;
    }
  }
}