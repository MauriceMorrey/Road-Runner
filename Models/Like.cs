using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace road_runner.Models
{
    public  class Like : BaseEntity {

        public int likeId { get;set; }

        public int userId { get;set; }

        public User user { get;set; }

        public int postId { get;set; }

        public Post post { get;set; }
    }
}