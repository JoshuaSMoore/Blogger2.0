using System.ComponentModel.DataAnnotations;
using Blogger.Models;

namespace Blogger

{
 public class Comment
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(30)]
        public string CreatorId { get; set; }
        [Required]
        [MaxLength(240)]
        public string Body { get; set; }
        [Required]
        public int BlogId { get; set; }
        public Profile Creator { get; set; }

        public string Title { get; set; }

        public bool Published { get; set; }

    }
}