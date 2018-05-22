using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace road_runner.Models
{
    public class FriendB : BaseEntity
    {
        [Key]
        public int friendBId { get; set; }

        public int userId { get; set; }

        public User friend2 { get; set; }

    }
}