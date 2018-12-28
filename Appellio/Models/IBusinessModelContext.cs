using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Appellio.Models
{
    public interface IBusinessModelContext
    {
        DbSet<Word> Words { get; set; }
        DbSet<Album> Albums { get; set; }
    }
}
