using Microsoft.EntityFrameworkCore;

namespace road_runner.Models
{
    public class RoadRunnerContext : DbContext
    {
        // base() calls the parent class' constructor passing the "options" parameter along
        public RoadRunnerContext(DbContextOptions<RoadRunnerContext> options) : base(options) { }

	    public DbSet<User> users {get; set;}
	    public DbSet<Trip> trips {get; set;}
	    public DbSet<Runner> runners {get; set;}
	    public DbSet<Friend> friends {get; set;}
	    public DbSet<Feature> features {get; set;}
        public DbSet<Post> posts {get; set;}
        public DbSet<Like> likes {get; set;}

    }
}