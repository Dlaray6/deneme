using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EduTrack.Domain
{
    public class User
    {
        public int Id { get; set; } // PK

        [Required]
        [StringLength(11, MinimumLength = 11)]
        public string TcNo { get; set; } = string.Empty;

        [Required]
        public string Name { get; set; } = string.Empty; // ÖNEMLİ: Bu eksikti!

        [Required]
        public string Password { get; set; } = string.Empty;

        [Required]
        public string Role { get; set; } = string.Empty; // "Student", "Teacher", "Idare"

        // Öğrenci için okul numarası
        public string? SchoolNumber { get; set; }

        // Öğretmen için telefon
        public string? PhoneNumber { get; set; }

        public int? SchoolId { get; set; }
        public School? School { get; set; }

        public ICollection<ClassUser> ClassUsers { get; set; } = new List<ClassUser>();
    }
}