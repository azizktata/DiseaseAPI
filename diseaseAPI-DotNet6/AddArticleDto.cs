using System.ComponentModel.DataAnnotations;

namespace diseaseAPI_DotNet6
{
    public class AddArticleDto
    {
        
        public string title { get; set; }
        public string description { get; set; }

        public string url { get; set; }
        [Required]
        public int diseaseId { get; set; }
    }
}
