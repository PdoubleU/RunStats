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

        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }
        public int ExcerciseTypeId { get; set; }
        public virtual ExcerciseType? ExcerciseType { get; set; }
        public int WheatherId { get; set; }
        public virtual Weather? Weather { get; set; }
        public int ShoesId { get; set; }
        public virtual Shoes Shoes { get; set; }
    }
}
