using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace road_runner.Models
{
    public class Feature : BaseEntity
    {
        [Key]
        public int featureId { get; set; }

       public string location { get; set; }

       public string name { get; set; }

    }
}