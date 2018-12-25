using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Appellio.Models;
using Appellio.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Appellio.Controllers
{
    [Route("Albums")]
    public class AlbumsController : Controller
    {
        private readonly IAlbumRepository _albumRepository;
        private readonly IWordRepository _wordRepository;

        public AlbumsController(IAlbumRepository albumRepository, IWordRepository wordRepository)
        {
            _albumRepository = albumRepository;
            _wordRepository = wordRepository;
        }
        [Route("")]
        public IActionResult Index()
        {
            return View(_albumRepository.GetAll());
        }

        [Route("{id}")]
        public IActionResult Index(int id)
        {
            var words = _wordRepository.GetWordsByAlbumId(id);
            return View("Words", words);
        }
    }
}