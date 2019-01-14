using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Appellio.Models
{
    public interface IBusinessDbContext
    {
        DbSet<Album> Albums { get; set; }
        DbSet<Word> Words { get; set; }
        int SaveChanges();
    }
}
