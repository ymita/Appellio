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
        private readonly IWordRepository _wordRepository;

        public WordsController(IWordRepository wordRepository)
        {
            _wordRepository = wordRepository;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            IWord word = _wordRepository.GetWordById(id.Value);
            return View(word);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Edit(int Id, string Spelling, string Meaning, string Text, int AlbumId)
        {
            return RedirectToAction("Index", "Albums", new { id = Id });
        }
    }
}