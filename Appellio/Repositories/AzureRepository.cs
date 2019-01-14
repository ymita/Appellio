using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Appellio.Models;

namespace Appellio.Repositories
{
    public class AzureRepository : IRepository
    {
        private readonly IBusinessDbContext _context;
        public AzureRepository(IBusinessDbContext context)
        {
            _context = context;
        }

        public IEnumerable<IAlbum> getAlbums()
        {
            return _context.Albums.ToList();
        }

        public IAlbum getAlbumById(int id)
        {
            return _context.Albums.Find(id);
        }

        public void updateAlbum(int id, string title)
        {
            var album = _context.Albums.Find(id);
            album.Title = title;

            _context.SaveChanges();
        }

        public void deleteAlbumById(int id)
        {
            var album = _context.Albums.Find(id);
            _context.Albums.Remove(album);

            _context.SaveChanges();
        }

        public void createAlbum(string title, string owner)
        {
            var lastAlbum = _context.Albums.Last();
            var albumId = lastAlbum.Id + 1;
            _context.Albums.Add(new Album { Id = albumId, Title = title, Owner = owner });

            _context.SaveChanges();
        }

        public IEnumerable<IWord> getWordsByAlbumId(int albumId)
        {
            return _context.Words.Where(word => word.AlbumId.Equals(albumId)).ToList();
        }

        public IWord getWordById(int id)
        {
            return _context.Words.Find(id);
        }

        public void updateWord(int id, string spelling, string meaning, string text)
        {
            var wordToUpdate = _context.Words.Find(id);

            wordToUpdate.Spelling = spelling;
            wordToUpdate.Meaning = meaning;
            wordToUpdate.Text = text;

            _context.SaveChanges();
        }

        public void deleteWordById(int id)
        {
            var word = _context.Words.Find(id);
            _context.Words.Remove(word);

            _context.SaveChanges();
        }

        public void createWord(string spelling, string meaning, string text, int albumId)
        {
            var lastWord = _context.Words.Last();
            int id = lastWord.Id + 1;
            _context.Words.Add(new Word { Id = id, Spelling = spelling, Meaning = meaning, Text = text, AlbumId = albumId });

            _context.SaveChanges();
        }
    }
}
