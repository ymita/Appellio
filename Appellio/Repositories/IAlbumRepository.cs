using Appellio.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Appellio.Repositories
{
    public interface IAlbumRepository
    {
        IEnumerable<IAlbum> GetAll();
    }
}
