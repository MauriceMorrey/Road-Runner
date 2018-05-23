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

        public int senderId { get; set; }


        public int receiverId { get; set; }


        public bool accepted { get;set; }

    }
}