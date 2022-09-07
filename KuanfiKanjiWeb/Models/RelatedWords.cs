using System.ComponentModel.DataAnnotations;

namespace KuanfiKanjiWeb.Models
{
    public class RelatedWords
    {
        [Key]
        public int Id { get; set; }
        public string KanjiWord { get; set; }
        public string Writing { get; set; }
        public string Meaning { get; set; }
        public int Set { get; set; }
        public int LearnedCount { get; set; }
    }
}
