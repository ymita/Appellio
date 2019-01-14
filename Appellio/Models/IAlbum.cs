using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Appellio.Models
{
    public interface IAlbum
    {
        int Id { get; set; }
        [DisplayName("タイトル")]
        string Title { get; set; }
        string Owner { get; set; }
    }
}
