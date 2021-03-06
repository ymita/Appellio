﻿using System;
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

        // GET: Words/Details/5
        public ActionResult Details(int id)
        {
            var word = _repository.getWordById(id);
            return View(word);
        }

        // GET: Words/Create
        public ActionResult Create(int albumId)
        {
            var word = Activator.CreateInstance<Word>();
            word.AlbumId = albumId;
            return View(word);
        }

        // POST: Words/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateWord(IFormCollection collection)
        {
            string spelling = collection["Spelling"][0];
            string meaning = collection["Meaning"][0];
            string text = collection["Text"][0];
            string textMeaning = collection["TextMeaning"][0];
            int albumId = int.Parse(collection["AlbumId"][0]);

            try
            {
                // TODO: Add insert logic here
                _repository.createWord(spelling, meaning, text, textMeaning, albumId);

                return RedirectToAction("Words", "Albums", new { id = albumId });
                //return RedirectToAction(nameof(Index));
            }
            catch
            {
                //TO DO: エラー発生時に問題なくリダイレクトするか？
                return RedirectToAction("Words", "Albums", new { id = albumId });
                //return View();
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
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                string spelling = collection["Spelling"][0];
                string meaning = collection["Meaning"][0];
                string text = collection["Text"][0];
                string textMeaning = collection["TextMeaning"][0];
                int albumId = int.Parse(collection["AlbumId"][0]);
                
                //// TODO: Add update logic here
                _repository.updateWord(id, spelling, meaning, text, textMeaning);

                return RedirectToAction("Details", "Words", new { id });
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
                return RedirectToAction("Words", "Albums", new { id = albumId });
            }
            catch
            {
                return View();
            }
        }
    }
}