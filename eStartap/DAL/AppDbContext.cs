using eStartap.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace eStartap.DAL
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<Setting> Settings { get; set; }
        public DbSet<HomePage> HomePages { get; set; }
        public DbSet<Card> Cards { get; set; }


    }
}
