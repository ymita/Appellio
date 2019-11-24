using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Appellio.Models;
using Microsoft.EntityFrameworkCore;

namespace Appellio.Repositories
{
    public class AzureRepository : IRepository
    {
        private readonly IBusinessDbContext _context;
        public AzureRepository(IBusinessDbContext context)
        {
            _context = context;
        }

        public IEnumerable<IAlbum> getAlbums(string owner)
        {
            return _context.Albums.Where(album => album.Owner == owner).ToList();
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
            //アルバムの削除
            _context.Albums.Remove(album);
            //アルバムに紐づく単語の削除
            var wordsToDelete = _context.Words.Where(w => w.AlbumId == id).ToList();
            _context.Words.RemoveRange(wordsToDelete);

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

        public void updateWord(int id, string spelling, string meaning, string text, string textMeaning)
        {
            var wordToUpdate = _context.Words.Find(id);

            wordToUpdate.Spelling = spelling;
            wordToUpdate.Meaning = meaning;
            wordToUpdate.Text = text;
            wordToUpdate.TextMeaning = textMeaning;

            _context.SaveChanges();
        }

        public void deleteWordById(int id)
        {
            var word = _context.Words.Find(id);
            _context.Words.Remove(word);

            _context.SaveChanges();
        }

        public void createWord(string spelling, string meaning, string text, string textMeaning, int albumId)
        {
            try
            {
                var lastWord = _context.Words.Last();
                int id = lastWord.Id + 1;
                _context.Words.Add(new Word { Id = id, Spelling = spelling, Meaning = meaning, Text = text, TextMeaning = textMeaning, AlbumId = albumId });

                _context.SaveChanges();
            }
            catch (DbUpdateException dbUpdateEx)
            {
                System.Diagnostics.Debug.WriteLine(dbUpdateEx.Message);
            }
            catch (InvalidOperationException invalidOperationEx)
            {
                System.Diagnostics.Debug.WriteLine(invalidOperationEx.Message);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            
        }
    }
}
