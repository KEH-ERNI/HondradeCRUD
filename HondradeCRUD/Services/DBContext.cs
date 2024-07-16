using HondradeCRUD.Models;
using Microsoft.EntityFrameworkCore;

namespace HondradeCRUD.Services
{
    public class DBContext : DbContext
    {
        public DBContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Bootcamper> Bootcampers { get; set; }
    }
}
