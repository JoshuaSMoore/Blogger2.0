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

    internal Comment GetById(int commentId)
    {
      Comment foundComment = _commentsRepository.GetById(commentId);
      if(foundComment == null)
      {
        throw new Exception("Invalid Blog Id");
      }
      return foundComment;
    }
  public Comment EditComment(int commentId, Comment commentData)
  {
    var comment = GetById(commentId);

    comment.Body = commentData.Body ?? comment.Body;

    _commentsRepository.EditComment(commentId, commentData);
    return comment;

  }

  internal Comment RemoveComment(int commentId, string userId)
    {
      Comment foundComment = GetById(commentId);
      if(foundComment.CreatorId != userId)
      {
        throw new Exception("That aint your comment");
      }
      _commentsRepository.RemoveComment(commentId);
      return foundComment;
    }

     public List<Comment> GetCommentsByAccount(string userId)
    {
      return _commentsRepository.GetCommentsByAccount(userId);
    }
  }
}