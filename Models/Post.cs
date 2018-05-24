using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace road_runner.Models
{
    public  class Post : BaseEntity {

        public int postId { get;set; }
        public string content { get;set; }

        public int tripId {get;set;}
        public Trip trip {get;set;}

        public bool currentUser { get;set; }

        [ForeignKey("creator")]
        public int creatorId {get;set;}
        public User creator {get;set;}

        public List<Like> likes {get;set;}

        public Post(){
            likes = new List<Like>();
            created_at = DateTime.Now;
        }
    }
}