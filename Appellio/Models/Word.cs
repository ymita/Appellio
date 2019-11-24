using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Appellio.Models
{
    public class Word : IWord
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public string Spelling { get; set; }

        [Required]
        public string Meaning { get; set; }

        [Required]
        public string Text { get; set; }
        public string TextMeaning { get; set; }
        [Required]
        public int AlbumId { get; set; }
    }
}
