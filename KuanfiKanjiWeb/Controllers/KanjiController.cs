using KuanfiKanjiWeb.Data;
using KuanfiKanjiWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net;

namespace KuanfiKanjiWeb.Controllers
{
    public class KanjiModel
    {
        public List<Kanji> Kanjis { get; set; }
        public int CurrentPageIndex { get; set; }
        public int PageCount { get; set; }
    }
    public class Result
    {
        public string chi_vie_kanji { get; set; }
        public string kanji { get; set; }
        public int stroke_count { get; set; }
        public string meanings { get; set; }
        public string kun_readings { get; set; }
        public string on_readings { get; set; }
        public string jlpt { get; set; }
        public List<RelatedWords> related_words { get; set; }
    }
    public class Root
    {
        public string kanji { get; set; }
        public int grade { get; set; }
        public int stroke_count { get; set; }
        public List<string> meanings { get; set; }
        public List<string> kun_readings { get; set; }
        public List<string> on_readings { get; set; }
        public List<object> name_readings { get; set; }
        public int jlpt { get; set; }
        public string unicode { get; set; }
        public string heisig_en { get; set; }
    }

    public class KanjiController : Controller
    {
        private readonly ApplicationDbContext _db;

        public KanjiController(ApplicationDbContext db)
        {
            _db = db;
        }

        public List<SelectListItem> listKanjiSet()
        {
            List<SelectListItem> kanjiSet = new List<SelectListItem>();
            foreach (var item in _db.KanjiSet.ToList())
            {
                kanjiSet.Add(new SelectListItem { Value = item.Set.ToString(), Text = item.SetName });
            }
            return kanjiSet;
        }

        [HttpGet]
        public IActionResult Index(int? value, int currentPageIndex = 1)
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

            return View(this.GetKanjis(value, currentPageIndex));
        }

        private KanjiModel GetKanjis(int? value, int currentPage)
        {
            int maxRows = 10;

            KanjiModel kanjiModel = new KanjiModel();
            IEnumerable<Kanji> objKanjiList = _db.Kanji.Where(x => x.Set.Equals(value));

            kanjiModel.Kanjis = objKanjiList.Skip((currentPage - 1) * maxRows).Take(maxRows).ToList();

            double pageCount = (double)((decimal)objKanjiList.Count() / Convert.ToDecimal(maxRows));
            kanjiModel.PageCount = (int)Math.Ceiling(pageCount);

            kanjiModel.CurrentPageIndex = currentPage;

            return kanjiModel;
        }

        // GET
        public IActionResult Details(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kanjiFromDB = _db.Kanji.FirstOrDefault(x => x.Name.Equals(id));

            var kanjiFromAPI = GetRootFromJson(id);

            if (kanjiFromAPI == null)
            {
                return NotFound();
            }

            Root root = GetRootFromJson(kanjiFromAPI.kanji);
            List<RelatedWords> listRelated = GetRelatedWords(kanjiFromAPI.kanji);

            Result result = new Result()
            {
                chi_vie_kanji = "",
                kanji = root.kanji,
                jlpt = "N" + root.jlpt.ToString(),
                stroke_count = root.stroke_count,
                meanings = string.Join(", ", root.meanings),
                kun_readings = string.Join(", ", root.kun_readings),
                on_readings = string.Join(", ", root.on_readings),
                related_words = listRelated
            };

            if (kanjiFromDB == null)
            {
                result.chi_vie_kanji = "";
            }
            else
            {
                result.chi_vie_kanji = kanjiFromDB.Chi_Vie_Meaning;
            }

            return View(result);
        }

        public Root GetRootFromJson(string kanji)
        {
            var json = "";

            var url = "https://kanjiapi.dev/v1/kanji/" + kanji;

            var httpRequest = (HttpWebRequest)WebRequest.Create(url);

            httpRequest.Accept = "application/json";

            var httpRespone = (HttpWebResponse)httpRequest.GetResponse();
            using (var streamReader = new StreamReader(httpRespone.GetResponseStream()))
            {
                json = streamReader.ReadToEnd();
            }

            Root obj = JsonConvert.DeserializeObject<Root>(json);

            return obj;
        }

        public List<RelatedWords> GetRelatedWords(string kanji)
        {
            var relatedWords = _db.RelatedWords.Where(x => x.KanjiWord.Contains(kanji)).ToList();

            return relatedWords;
        }
    }
}
