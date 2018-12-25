using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Appellio.Models
{
    public class Album : IAlbum
    {
        public int Id { get; set; }
        public string Title { get; set; }
    }
}
