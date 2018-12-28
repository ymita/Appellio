using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Appellio.Models;

namespace Appellio.Repositories
{
    public class SqliteRepository : IRepository
    {
        private readonly IBusinessModelContext _context;
        public SqliteRepository(IBusinessModelContext context)
        {
            _context = context;
        }

        public IWord GetWordById(int id)
        {
            return _context.Words.Find(id);
        }

        public void AddWord(string spelling, string meaning, string text, int albumId)
        {
            var newWord = Activator.CreateInstance<Word>();
            int currentMaxId = _context.Words.Last().Id;

            newWord.Id = currentMaxId + 1;
            newWord.Spelling = spelling;
            newWord.Meaning = meaning;
            newWord.Text = text;
            newWord.AlbumId = albumId;

            _context.Words.Add(newWord);
            (_context as BusinessModelContext).SaveChanges();
        }

        public void UpdateWord(int id, string spelling, string meaning, string text, int albumId)
        {
            var word = _context.Words.Where(x => x.Id == id).First();

            if (word != null)
            {
                word.Spelling = spelling;
                word.Meaning = meaning;
                word.Text = text;
                word.AlbumId = albumId;
            }

            (_context as BusinessModelContext).SaveChanges();
        }

        public IEnumerable<IWord> GetWords()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IAlbum> GetAlbums()
        {
            return _context.Albums.ToList();
        }

        public IEnumerable<IWord> GetWordsByAlbumId(int albumId)
        {
            return _context.Words.Where(x => x.AlbumId == albumId).ToList();
        }

        public IAlbum GetAlbumById(int id)
        {
            return _context.Albums.Find(id);
        }

        public void UpdateAlbum(int id, string title)
        {
            _context.Albums.Find(id).Title = title;
            (_context as BusinessModelContext).SaveChanges();
        }
    }
}
