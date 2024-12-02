using Microsoft.EntityFrameworkCore;
using QLVT.Models;

namespace QLVT.Services
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions options): base(options) 
        {
            
        }
        public DbSet<Product> ListProduct {  get; set; }
        public DbSet<Import> ListImport { get; set; }
        
        public DbSet<Export> ListExport { get; set; }
    }
}
