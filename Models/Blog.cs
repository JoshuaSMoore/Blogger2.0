using System.ComponentModel.DataAnnotations;
using Blogger.Models;

namespace Blogger
 {
 public class Blog
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(30)]
        public string Title { get; set; }
        public string Body { get; set; }
        public string ImgUrl { get; set; }
        
        public bool Published { get; set; }
        public string CreatorId { get; set; }

        public Profile Creator { get; set; }
    }
  }