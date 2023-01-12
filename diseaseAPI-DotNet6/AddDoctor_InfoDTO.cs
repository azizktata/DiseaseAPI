using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations.Schema;

namespace diseaseAPI_DotNet6
{
    public class AddDoctor_InfoDTO
    {
        public string name { get; set; }

        public string speciality { get; set; }

        public string location { get; set; }

        [Required]
        public int diseaseId { get; set; }
    }
}
