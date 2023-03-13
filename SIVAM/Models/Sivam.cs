using System.ComponentModel.DataAnnotations;

namespace SIVAM.Models
{
    public class Sivam
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int RollNo { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Class { get; set; }

    }
}
