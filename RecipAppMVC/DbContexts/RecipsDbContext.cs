using Microsoft.EntityFrameworkCore;
using RecipAppMVC.Models;

namespace RecipAppMVC.DbContexts
{
    public class RecipsDbContext : DbContext
    {
        private string _connectionstring = "Server=127.0.0.1,1433;Database=RecipAppDB;User ID=sa;Password=Z12345678!a;Encrypt=False;";

        public DbSet<RecipModel> Recips { get; set; }
        public DbSet<LikedRecip> LikedRecip {get; set;}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionstring);
        }
    }
}
