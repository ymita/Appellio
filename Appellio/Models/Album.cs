using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Appellio.Models
{
    public class Album : IAlbum
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        [Display(Name = "タイトル")]
        public string Title { get; set; }
        [Required]
        public string Owner { get; set; }
    }
}
