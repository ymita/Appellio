using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Appellio.Models
{
    public class BusinessDbContext : DbContext, IBusinessDbContext
    {
        public BusinessDbContext()
        {

        }

        public BusinessDbContext(DbContextOptions<BusinessDbContext> options)
            : base(options)
        {
        }

        public DbSet<Album> Albums { get; set; }
        public DbSet<Word> Words { get; set; }

        public override int SaveChanges()
        {
            return base.SaveChanges();
        }
    }
}
