using System.Data;
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
  }
}