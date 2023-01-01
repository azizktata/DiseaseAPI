using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string firstName { get; set; }

        public string lastName { get; set; }

        public string location { get; set; }

        public string email { get; set; }

        public string password { get; set; }
    }
}
