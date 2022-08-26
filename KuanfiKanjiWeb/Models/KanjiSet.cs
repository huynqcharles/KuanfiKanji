using System.ComponentModel.DataAnnotations;

namespace KuanfiKanjiWeb.Models
{
    public class KanjiSet
    {
        [Key]
        public int Id { get; set; }
        public int Set { get; set; }
        public string SetName { get; set; }
    }
}
