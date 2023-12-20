using System.ComponentModel.DataAnnotations;

namespace RunStats.Models
{
    public class Weather
    {
        [Key]
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Location { get; set; }
        public double Temperature { get; set; }
        public int Moisture { get; set; }
        public int Clouds { get; set; }
        public int WindSpeed { get; set; }
    }
}
