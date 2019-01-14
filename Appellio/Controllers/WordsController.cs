using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Appellio.Models;
using Appellio.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Appellio.Controllers
{
    [Authorize]
    public class WordsController : Controller
    {
        private readonly IRepository _repository;
        public WordsController(IRepository repository)
        {
            _repository = repository;
        }

        // GET: Words
        public ActionResult Index()
        {
            return RedirectToAction("Index", "Albums");
            //return View();
        }

        //public ActionResult WordsByAlbumId(int id)
        //{
        //    var words = _repository.getWordsByAlbumId(id);
        //    return View(words);
        //}

        // GET: Words/Details/5
        public ActionResult Details(int id)
        {
            var word = _repository.getWordById(id);
            return View(word);
        }

        // GET: Words/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Words/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here
                string spelling = collection["Spelling"][0];
                string meaning = collection["Meaning"][0];
                string text = collection["Text"][0];
                int albumId = int.Parse(collection["AlbumId"][0]);

                _repository.createWord(spelling, meaning, text, albumId);

                return RedirectToAction("Words", "Albums", new { id = albumId });
                //return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Words/Edit/5
        public ActionResult Edit(int id)
        {
            var word = _repository.getWordById(id);
            return View(word);
        }

        // POST: Words/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Word word /*IFormCollection collection*/)
        {
            try
            {
                // TODO: Add update logic here
                _repository.updateWord(id, word);

                return RedirectToAction("Details", "Words", new { id = word.AlbumId });
                //return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Words/Delete/5
        public ActionResult Delete(int id)
        {
            var word = _repository.getWordById(id);

            return View(word);
        }

        // POST: Words/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                int albumId = int.Parse(collection["AlbumId"][0]);
                // TODO: Add delete logic here
                _repository.deleteWordById(id);
                return RedirectToAction("Details", "Albums", new { id = albumId });
                //return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}