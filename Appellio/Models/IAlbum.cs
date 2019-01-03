using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Appellio.Models
{
    public interface IAlbum
    {
        int Id { get; set; }
        string Title { get; set; }
        string Owner { get; set; }
    }
}
