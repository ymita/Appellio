using Appellio.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Appellio.Repositories
{
    public class AlbumRepository : IAlbumRepository
    {
        public IEnumerable<IAlbum> GetAll()
        {
            List<IAlbum> albums = new List<IAlbum>();

            albums.Add(new Album() { Id = 1, Title = "Album 1" });
            albums.Add(new Album() { Id = 2, Title = "Album 2" });
            albums.Add(new Album() { Id = 3, Title = "Album 3" });

            return albums;
        }
    }
}
