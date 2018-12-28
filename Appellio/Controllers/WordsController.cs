using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Appellio.Models;
using Appellio.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Appellio.Controllers
{
    public class WordsController : Controller
    {
        private readonly IRepository _repository;
        public WordsController(IRepository repository)
        {
            _repository = repository;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Add(string spelling, string meaning, string text, int albumId)
        {
            // Save to DbContext
            _repository.AddWord(spelling, meaning, text, albumId);
            //_repository.SaveWord(Id, Spelling, Meaning, Text, AlbumId);
            // And then, return to the album the word is belonged to.
            return RedirectToAction("Index", "Albums", new { id = albumId });
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            IWord word = _repository.GetWordById(id);
            return View(word);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Edit(int id, string spelling, string meaning, string text, int albumId)
        {
            // Save to DbContext
            _repository.UpdateWord(id, spelling, meaning, text, albumId);
            // And then, return to the album the word is belonged to.
            return RedirectToAction("Index", "Albums", new { id = albumId });
        }
    }
}