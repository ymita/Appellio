using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Appellio.Models
{
    public interface IWord
    {
        int Id { get; set; }
        string Spelling { get; set; }
        string Meaning { get; set; }
        string Text { get; set; }
        int AlbumId { get; set; }
    }
}
