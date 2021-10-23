using System.Collections.Generic;
using System.Data;
using System.Linq;
using Blogger.Models;
using Dapper;

namespace Blogger.Repositories
{
  public class CommentsRepository
  {

    private readonly IDbConnection _db;

    public CommentsRepository(IDbConnection db)
    {
      _db = db;
    }
    public Comment CreateComment(Comment commentData)
    {
      var sql = @"
      INSERT INTO comments(
       creatorId,
       body,
       blog
       )
       VALUES (
         @CreatorId,
         @Body,
         @Blog
       );
       SELECT LAST_INSERT_ID();
        ";
        int id = _db.ExecuteScalar<int>(sql, commentData);
        commentData.Id = id;
        return commentData;
    }

    public Comment GetById(int commentId)
    {
      string sql = @"
      SELECT
      c.*,
      a.*
      FROM comments c
      JOIN accounts a on c.creatorId = a.id
      WHERE c.id = @commentId;
      ";
      return _db.Query<Comment, Account, Comment>(sql, (c, a) =>
    {
      c.Creator = a;
      return c;
    }, new{commentId}).FirstOrDefault(); 
    }


    public List<Comment> GetCommentsByBlog(int blog)
    {
      string sql = @"
      SELECT
      c.*,
      a.*
      FROM comments c
      JOIN accounts a on c.creatorId = a.id
      WHERE c.blog = @Blog;
      ";
      return _db.Query<Comment, Account, Comment>(sql, (c, a) =>
    {
      c.Creator = a;
      return c;
    }, new{blog}).ToList(); 
    }
    public Comment EditComment(int commentId, Comment commentData)
    {
      commentData.Id = commentId;
      var sql = @"
      UPDATE comments
      SET
      body = @Body
      WHERE id = @Id";
      var rowsAffected = _db.Execute(sql, commentData);
      if (rowsAffected == 0)
      {
        throw new System.Exception("The Update has Failed");
      }
      return commentData;
    }

    internal List<Comment> GetCommentsByAccount(string userId)
    {
      string sql = "SELECT * FROM comments c WHERE c.creatorId = @userId";
      return _db.Query<Comment>(sql, new{userId}).ToList();
    }

    internal void RemoveComment(int commentId)
    {
      string sql = "DELETE FROM comments WHERE id = @id LIMIT 1;";
      var affectedRows = _db.Execute(sql, new {commentId});
      if(affectedRows == 0)
      {
        throw new System.Exception("Cannot Delete a comment that is already delorted");
      }
    }
  }
}