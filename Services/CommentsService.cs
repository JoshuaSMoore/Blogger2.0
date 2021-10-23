using Blogger.Repositories;

namespace Blogger.Services
{
  public class CommentsService
  {

  private readonly CommentsRepository _commentsRepository;
     public CommentsService(CommentsRepository commentsRepository)
  {
    _commentsRepository = commentsRepository;
  }
    public Comment CreateComment(Comment commentData)
    {
      return _commentsRepository.CreateComment(commentData);
    }
  }
}