using KuanfiKanjiWeb.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json.Linq;
using System.Runtime.CompilerServices;

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

        // Get info of lession set: number of words learned, not learned...
        public (int, int, int) getLessonInfo(int set)
        {
            int totalWords = _db.RelatedWords.Where(x => x.Set.Equals(set))
                .Select(x => x.KanjiWord)
                .Distinct().Count();
            int learned = _db.RelatedWords.Where(x => x.Set.Equals(set))
                .Where(x => x.LearnedCount > 0)
                .Select(x => x.KanjiWord)
                .Distinct().Count();
            int notLearned = _db.RelatedWords.Where(x => x.Set.Equals(set))
                .Where(x => x.LearnedCount == 0)
                .Select(x => x.KanjiWord)
                .Distinct().Count();
            return (totalWords, learned, notLearned);
        }

        public IActionResult Index(int? value)
        {
            var kanjiSet = listKanjiSet();

            if (value != null)
            {
                kanjiSet.Where(i => i.Value == value.ToString()).First().Selected = true;
            }
            else
            {
                value = 1;
            }

            ViewBag.kanjiSet = kanjiSet;

            (int totalWords, int learned, int notLearned) = getLessonInfo((int)value);

            ViewBag.totalWords = totalWords;
            ViewBag.learned = learned;
            ViewBag.notLearned = notLearned;

            return View();
        }
    }
}
