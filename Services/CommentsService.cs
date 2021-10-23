using System;
using System.Collections.Generic;
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

    public List<Comment> GetCommentsByBlog(int blogId)
    {
      return _commentsRepository.GetCommentsByBlog(blogId);
    }

    public Comment GetbyId(int commentId)
    {
      Comment foundComment = _commentsRepository.GetById(commentId);
      if(foundComment == null)
    {
    throw new Exception("Nope no comment by this Id");
  }
  return foundComment;
  }
  public Comment EditComment(int commentId, Comment commentData)
  {
    var comment = GetbyId(commentId);

    comment.Body = commentData.Body ?? comment.Body;

    _commentsRepository.EditComment(commentId, commentData);
    return comment;

  }

  internal void RemoveComment(int commentId, string userId)
    {
      Comment foundComment = GetbyId(commentId);
      if(foundComment.CreatorId != userId)
      {
        throw new Exception("That aint your team");
      }
      _commentsRepository.RemoveComment(commentId);
    }
  }
}