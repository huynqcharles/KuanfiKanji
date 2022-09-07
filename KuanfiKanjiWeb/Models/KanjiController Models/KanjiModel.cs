namespace KuanfiKanjiWeb.Models
{
    public class KanjiModel
    {
        public List<Kanji> Kanjis { get; set; }
        public int CurrentPageIndex { get; set; }
        public int PageCount { get; set; }
    }
}
