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
  }
}