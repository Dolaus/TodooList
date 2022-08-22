using Microsoft.EntityFrameworkCore;
using TodooList.Models;

namespace TodooList.Data
{
    public class AppDBContext:DbContext
    {
        
        public DbSet<User> User { get; set; }
        public DbSet<ToDo> ToDo { get; set; }

        public AppDBContext(DbContextOptions<AppDBContext> options) 
           : base(options) 
        { Database.EnsureCreated(); }
    }
}
