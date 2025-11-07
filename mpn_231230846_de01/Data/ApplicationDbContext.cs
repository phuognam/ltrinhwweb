using Microsoft.AspNetCore;
using mpn_231230846_de01.Models;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
namespace mpn_231230846_de01.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<MpnComputer> MpnComputer { get; set; }
    }

}
