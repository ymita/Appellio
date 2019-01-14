using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Appellio.Models
{
    public interface IWord
    {
        int Id { get; set; }
        [DisplayName("単語")]
        string Spelling { get; set; }
        [DisplayName("意味")]
        string Meaning { get; set; }
        [DisplayName("例文")]
        string Text { get; set; }
        int AlbumId { get; set; }
    }
}
