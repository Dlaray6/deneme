using EduTrack.Domain;
using System.ComponentModel.DataAnnotations;

public class User
{
    public int Id { get; set; } // PK

    [Required, StringLength(11, MinimumLength = 11)]
    public string TcNo { get; set; } = string.Empty;

    [Required]
    public string Name { get; set; } = string.Empty;

    [Required]
    public string Password { get; set; } = string.Empty;

    [Required]
    public string Role { get; set; } = string.Empty; // "Student", "Teacher", "Idare"

    public string? SchoolNumber { get; set; }  // Öğrenci için okul numarası
    public string? PhoneNumber { get; set; }   // Öğretmen için telefon

    // SchoolId nullable olabilir (kayıt aşamasına bağlı)
    public int? SchoolId { get; set; }
    public School? School { get; set; }

    public ICollection<ClassUser> ClassUsers { get; set; } = new List<ClassUser>();
}
