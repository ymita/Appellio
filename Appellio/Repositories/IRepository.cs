using Appellio.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Appellio.Repositories
{
    public interface IRepository
    {
        IEnumerable<IWord> GetWords();

        IWord GetWordById(int id);

        void UpdateWord(int id, string spelling, string meaning, string text, int albumId);

        //void AddWord(string spelling, string meaning, string text, int albumId);
        void CreateWord(string spelling, string meaning, string text, int albumId);
        IEnumerable<IAlbum> GetAlbums();

        IEnumerable<IWord> GetWordsByAlbumId(int albumId);

        IAlbum GetAlbumById(int id);

        void UpdateAlbum(int id, string title);
    }
}
