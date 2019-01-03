using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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

        private string getOwnerInfo()
        {
            string typeStr = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier";
            Claim claim = User.Claims.Where(x => x.Type == typeStr).First();
            return claim.Value;
        }

        [Route("")]
        public IActionResult Index()
        {
            string owner = getOwnerInfo();
            var albums = _repository.GetAlbums(owner);

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
            string owner = getOwnerInfo();
            _repository.CreateAlbum(title, owner);
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