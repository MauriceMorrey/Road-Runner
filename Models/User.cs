using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace road_runner.Models {
 public abstract class BaseEntity {

       public DateTime created_at {get; set;}
       public DateTime updated_at {get; set;}

 }
	public class User : BaseEntity{
            [Key]
		public int userId {get; set;}   
		public string first_name {get; set;}	
		public string last_name {get; set;}	
		public string username {get; set;}	
        public string email {get; set;}
        public string password {get; set;}
        
        public List<Trip> planned { get;set; }

        public List<Runner> attended { get;set; }
        public List<Friend> friends { get;set; }
        
        public User(){
            created_at = DateTime.Now;
            updated_at = DateTime.Now;
            planned = new List<Trip>();
            attended = new List<Runner>();
            friends = new List<Friend>();
        }

	}
}