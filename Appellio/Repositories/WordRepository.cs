using Appellio.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Appellio.Repositories
{
    public class WordRepository : IWordRepository
    {
        public IEnumerable<IWord> GetAll()
        {
            //Generate and return dummy data. This should be comming from database.
            List<IWord> words = new List<IWord>();

            words.Add(new Word
            {
                Id = 1,
                Spelling = "pen",
                Meaning = "ペン1",
                Text = "I am a pen. Are you?",
                AlbumId = 1
            });

            words.Add(new Word
            {
                Id = 2,
                Spelling = "pen",
                Meaning = "ペン2",
                Text = "I am a pen. Are you?",
                AlbumId = 2
            });

            words.Add(new Word
            {
                Id = 3,
                Spelling = "pen",
                Meaning = "ペン3",
                Text = "I am a pen. Are you?",
                AlbumId = 3
            });

            words.Add(new Word
            {
                Id = 4,
                Spelling = "pen",
                Meaning = "ペン4",
                Text = "I am a pen. Are you?",
                AlbumId = 1
            });

            words.Add(new Word
            {
                Id = 5,
                Spelling = "pen",
                Meaning = "ペン5",
                Text = "I am a pen. Are you?",
                AlbumId = 2
            });

            return words;
        }

        public IWord GetWordById(int id)
        {
            var word = GetAll().Where(x => x.Id == id).FirstOrDefault();
            return word;
        }

        public IEnumerable<IWord> GetWordsByAlbumId(int id)
        {
            var res = GetAll().Where(x => x.AlbumId == id).ToList();
            return res;
        }
    }
}
