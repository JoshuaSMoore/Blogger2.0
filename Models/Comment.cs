using Blogger.Models;

namespace Blogger

{
  public class Comment
    {
        public int Id { get; set; }
        public string CreatorId { get; set; }
        public string Body { get; set; }
        public int Blog { get; set; }
        public Profile Creator { get; set; }

        public string Title { get; set; }

        public bool Published { get; set; }

    }
}