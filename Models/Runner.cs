using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace road_runner.Models
{
    public class Runner : BaseEntity
    {
        [Key]
        public int runnerId { get; set; }

        public int userId { get; set; }

        public User user { get; set; }
        public int tripId { get; set; }

        public Trip trip { get; set; }

    }
}