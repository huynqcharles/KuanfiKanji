namespace KuanfiKanjiWeb.Models
{
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
}
