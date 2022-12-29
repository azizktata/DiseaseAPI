using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace diseaseAPI_DotNet6.Models
{
    public class Doctor_info
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string name { get; set; }

        public string speciality { get; set; }

        public string location { get; set; }

        [ForeignKey("Disease")]
        public int diseaseId { get; set; }
    }
}
