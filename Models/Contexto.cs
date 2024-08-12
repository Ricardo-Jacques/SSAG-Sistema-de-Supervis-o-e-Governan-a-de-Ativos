using Microsoft.EntityFrameworkCore;
using SiteMVC.Models;

namespace SiteMVC.Models
{
    public class Contexto : DbContext
    {
        public Contexto(DbContextOptions<Contexto> options) : base(options)
        {

        }
        public DbSet<Item> Items { get; set; }
        public DbSet<SiteMVC.Models.Usuario> Usuario { get; set; } = default!;
    }
}