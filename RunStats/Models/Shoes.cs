using System.ComponentModel.DataAnnotations;

namespace RunStats.Models
{
    public class Shoes
    {
        [Key]
        public int Id { get; set; }
        public string Model { get; set; }
        public int TotalDistance { get; set; }
        public int ShoesTypeId { get; set; }
        public virtual ShoesType? ShoesType { get; set; }
        public string UserId { get; set; }
        public virtual ApplicationUser? User { get; set; }
    }
}
