namespace KuanfiKanjiWeb.Models
{
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
}
