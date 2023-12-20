using System.ComponentModel.DataAnnotations;

namespace RunStats.Models
{
    public class ShoesType
    {
        [Key]
        public int Id { get; set; }
        public string TypeName { get; set; }
    }
}
