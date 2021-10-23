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

    public List<Comment> GetCommentsByBlog(int blogId)
    {
      string sql = @"
      SELECT * FROM comments c WHERE c.blogId = @blogId";
      return _db.Query<Comment>(sql, new {blogId}).ToList();
    }
  }
}