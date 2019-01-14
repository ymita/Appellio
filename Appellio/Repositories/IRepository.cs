using Appellio.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Appellio.Repositories
{
    public interface IRepository
    {
        IEnumerable<IAlbum> getAlbums(string owner);
        IAlbum getAlbumById(int id);
        void updateAlbum(int id, string title);
        void deleteAlbumById(int id);
        void createAlbum(string title, string owner);

        IEnumerable<IWord> getWordsByAlbumId(int albumId);
        IWord getWordById (int id);
        void updateWord(int id, string spelling, string meaning, string text);
        void deleteWordById(int id);
        void createWord(string spelling, string meaning, string text, int albumId);
    }
}
