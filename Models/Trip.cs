using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace road_runner.Models {
    public class Trip : BaseEntity
    {
        [Key]
        public int tripId { get; set; }

        public string start_point { get; set; }
        public string destination { get; set; }
        
        public string description { get; set; }

        public DateTime start_date { get; set; }

        public DateTime end_date { get; set; }

        public bool currentUser { get; set; }

        public int userId { get;set; }

        public User planner { get;set; }

        public List<Runner> runners { get; set; }
        public List<Feature> features { get; set; }
        public List<Post> posts {get;set;}

        public Trip()
        {
            runners = new List<Runner>();
            features = new List<Feature>();
            posts = new List<Post>();
        }
    
    }
}