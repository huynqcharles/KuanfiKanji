using System.ComponentModel.DataAnnotations;

namespace KuanfiKanjiWeb.Models
{
    public class Kanji
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Chi_Vie_Meaning { get; set; }
        public int Set { get; set; }
    }
}
