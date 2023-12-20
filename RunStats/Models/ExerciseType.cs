using System.ComponentModel.DataAnnotations;

namespace RunStats.Models
{
    public class ExerciseType
    {
        [Key]
        public int Id { get; set; }
        public string ExerciseName { get; set; }
    }
}
