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
        private readonly IRepository _repository;

        public AlbumsController(IRepository repository)
        {
            _repository = repository;
        }

        [Route("")]
        public IActionResult Index()
        {
            var albums = _repository.GetAlbums();
            return View(albums);
        }

        [Route("{id}")]
        public IActionResult Index(int id)
        {
            ViewData["AlbumTitle"] = _repository.GetAlbumById(id).Title;
            var words = _repository.GetWordsByAlbumId(id);
            return View("Words", words);
        }

        [HttpPost]
        [Route("/Create")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        [Route("/CreateData")]
        public IActionResult CreateData(string title)
        {
            _repository.CreateAlbum(title);
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Route("Edit/{id}")]
        public IActionResult Edit(int id)
        {
            IAlbum album = _repository.GetAlbumById(id);
            return View(album);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        [Route("Edit/{id}")]
        public IActionResult Edit(int id, string title)
        {
            _repository.UpdateAlbum(id, title);
            return RedirectToAction("Index");
        }
    }
}