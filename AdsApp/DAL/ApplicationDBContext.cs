using DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL
{
    public class ApplicationDBContext :DbContext
    {
        public DbSet<Ad> Ads { get; set; }
        public ApplicationDBContext (DbContextOptions<ApplicationDBContext> options)
            : base(options)
        { }
    }
}
