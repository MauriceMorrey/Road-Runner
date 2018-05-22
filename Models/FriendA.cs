using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace road_runner.Models
{
    public class FriendA : BaseEntity
    {
        [Key]
        public int friendAId { get; set; }

        public int userId { get; set; }

        public User friend1 { get; set; }

    }
}