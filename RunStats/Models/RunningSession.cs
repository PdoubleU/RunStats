﻿using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.InteropServices;

namespace RunStats.Models
{
    public class RunningSession
    {
        [Key]
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int Distance { get; set; }
        public int? Time { get; set; }

        public string UserId { get; set; }
        public virtual ApplicationUser? User { get; set; }
        public int? ExerciseTypeId { get; set; }
        public virtual ExerciseType? ExerciseType { get; set; }
        public int? WeatherId { get; set; }
        public virtual Weather? Weather { get; set; }
        public int? ShoesId { get; set; }
        public virtual Shoes? Shoes { get; set; }
        [NotMapped]
        public virtual string? Pace { get; set; }
    }
}
