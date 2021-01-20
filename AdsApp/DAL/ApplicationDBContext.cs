using DAL.EFModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
