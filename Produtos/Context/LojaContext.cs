using Microsoft.EntityFrameworkCore;
using Produtos.Model;

namespace Produtos.Context
{
    public class LojaContext : DbContext
    {
        public LojaContext(DbContextOptions<LojaContext> options) : base(options) 
        {

        }

        public DbSet<Produto> Produtos {  get; set; } 
    }
}
