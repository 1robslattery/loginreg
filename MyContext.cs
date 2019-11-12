using Microsoft.EntityFrameworkCore;
using Unicorns.Models;
 
namespace Unicorns.Models

{
    public class MyContext : DbContext
    {
        // base() calls the parent class' constructor passing the "options" parameter along
        public MyContext(DbContextOptions options) : base(options) { }

		// Users 
		// "dishes" table is represented by this DbSet "dish"
		// "DbSet<User>" is being referenced from public class truck w/n User.cs
		public DbSet<User> Users {get;set;}
    }
}