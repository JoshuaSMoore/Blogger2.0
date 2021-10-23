using System.Collections.Generic;
using System.Data;
using System.Linq;
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
       blogId
       )
       VALUES (
         @CreatorId,
         @Body,
         @BlogId
       );
       SELECT LAST_INSERT_ID();
        ";
        int id = _db.ExecuteScalar<int>(sql, commentData);
        commentData.Id = id;
        return commentData;
    }

    public Comment GetById(int commentId)
    {
      return _db.QueryFirstOrDefault<Comment>("SELECT * FROM comments WHERE id = @commentId", new {commentId});
    }

    public List<Comment> GetCommentsByBlog(int blogId)
    {
      string sql = @"
      SELECT * FROM comments c WHERE c.blogId = @blogId";
      return _db.Query<Comment>(sql, new {blogId}).ToList();
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

   public void RemoveComment(int commentId)
    {
      string sql = "DELETE FROM comments WHERE id = @commentId LIMIT 1;";
      var affectedRows = _db.Execute(sql, new {commentId});
      if(affectedRows == 0)
      {
        throw new System.Exception("Cannot Delete a comment that is already delorted");
      }
    }
  }
}