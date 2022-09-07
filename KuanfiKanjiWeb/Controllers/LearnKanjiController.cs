﻿using KuanfiKanjiWeb.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace KuanfiKanjiWeb.Controllers
{
    public class LearnKanjiController : Controller
    {
        private readonly ApplicationDbContext _db;

        public LearnKanjiController(ApplicationDbContext db)
        {
            _db = db;
        }

        // Get list of kanji set from database
        public List<SelectListItem> listKanjiSet()
        {
            List<SelectListItem> kanjiSet = new List<SelectListItem>();
            foreach (var item in _db.KanjiSet.ToList())
            {
                kanjiSet.Add(new SelectListItem { Value = item.Set.ToString(), Text = item.SetName });
            }
            return kanjiSet;
        }

        public IActionResult Index()
        {
            var kanjiSet = listKanjiSet();
            ViewBag.kanjiSet = kanjiSet;
            return View();
        }
    }
}