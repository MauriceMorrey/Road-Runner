using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace road_runner.Models
{
    public class Friend : BaseEntity
    {
        [Key]
        public int friendId { get; set; }

        public int friendAId { get; set; }

        public FriendA friend_a { get; set; }

        public int friendBId { get; set; }

        public FriendB friend_b { get; set; }

    }
}