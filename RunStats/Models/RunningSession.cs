using System.ComponentModel.DataAnnotations;

namespace RunStats.Models
{
    public class RunningSession
    {
        [Key]
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public float Distance { get; set; }
        public float Time { get; set; }

    }
}
