using System.ComponentModel.DataAnnotations;

namespace RunStats.Models
{
    public class ExcerciseType
    {
        [Key]
        public int Id { get; set; }
        public string ExcerciseName { get; set; }
    }
}
