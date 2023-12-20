using System.ComponentModel.DataAnnotations;

namespace RunStats.Models
{
    public class Shoes
    {
        [Key]
        public int Id { get; set; }
        public string Model { get; set; }
        public float TotalDistance { get; set; }
    }
}
