using Microsoft.EntityFrameworkCore;

namespace LoginRegistration.Models
{
    public class MyContext : DbContext
    {
        public MyContext(DbContextOptions options) : base(options) { }
        // add DB sets here
        public DbSet<Login> logins {get; set;}
        public DbSet<Register> registers {get; set;}
    }

}