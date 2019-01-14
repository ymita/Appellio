using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Appellio.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Appellio.Controllers
{
    [Authorize]
    public class AlbumsController : Controller
    {
        private readonly IRepository _repository;
        public AlbumsController(IRepository repository)
        {
            _repository = repository;
        }

        // GET: Albums
        public ActionResult Index()
        {
            var albums = _repository.getAlbums();
            return View(albums);
        }

        public ActionResult Words(int id)
        {
            ViewData["AlbumTitle"] = _repository.getAlbumById(id).Title;
            TempData["AlbumId"] = id;
            var words = _repository.getWordsByAlbumId(id);
            return View(words);
        }

        // GET: Albums/Details/5
        public ActionResult Details(int id)
        {
            var album = _repository.getAlbumById(id);
            return View(album);
        }

        // GET: Albums/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Albums/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here
                string title = collection["Title"][0];
                string owner = getOwnerInfo();
                _repository.createAlbum(title, owner);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Albums/Edit/5
        public ActionResult Edit(int id)
        {
            var album = _repository.getAlbumById(id);
            return View(album);
        }

        // POST: Albums/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                //TO DO: id and title should be cached here...
                string title = collection["Title"][0];
                _repository.updateAlbum(id, title);

                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Albums/Delete/5
        public ActionResult Delete(int id)
        {
            var album = _repository.getAlbumById(id);

            return View(album);
        }

        // POST: Albums/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                _repository.deleteAlbumById(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        private string getOwnerInfo()
        {
            string typeStr = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier";
            Claim claim = User.Claims.Where(x => x.Type == typeStr).First();
            return claim.Value;
        }
    }
}