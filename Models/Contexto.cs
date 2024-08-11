using Microsoft.EntityFrameworkCore;

namespace SiteMVC.Models
{
    public class Contexto : DbContext
    {
        public Contexto(DbContextOptions<Contexto> options) : base(options)
        {

        }
        public DbSet<Item> Items { get; set; }
    }
}