using Appellio.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Appellio.Repositories
{
    public interface IWordRepository
    {
        IEnumerable<IWord> GetAll();
        
        IEnumerable<IWord> GetWordsByAlbumId(int albumId);
        IWord GetWordById(int wordId);
    }
}
