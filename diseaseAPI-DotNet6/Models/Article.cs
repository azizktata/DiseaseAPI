using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace diseaseAPI_DotNet6.Models
{
    public class Article
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string title { get; set; }
        public string description { get; set; }

        public string url { get; set; }

        [ForeignKey("Disease")]
        public int diseaseId { get; set; }
    }
}
