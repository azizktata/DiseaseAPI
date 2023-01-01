using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public class Disease
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string diseaseName { get; set; }

        public virtual HashSet<Article> articles { get; set; }

        public virtual HashSet<Doctor_info> doctors { get; set; }
    }
}
