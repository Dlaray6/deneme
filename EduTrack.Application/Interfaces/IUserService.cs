using System.Collections.Generic;
using System.Threading.Tasks;
using EduTrack.Domain;

namespace EduTrack.Application.Interfaces
{
    public interface IUserService
    {
        // Yeni kullanıcı ekler (veritabanına ekleme işlemi başlatılır)
        Task AddUserAsync(User user);

        // TC numarasına göre kullanıcı getirir
        Task<User?> GetUserByTCAsync(string tc);

        // Rolüne göre kullanıcıları listeler (Örn: "Student", "Teacher", "Admin")
        Task<IEnumerable<User>> GetUsersByRoleAsync(string role);

        // Kullanıcı şifresini doğrular (true/false döner)
        Task<bool> CheckPasswordAsync(string tc, string password);

        // Veritabanındaki değişiklikleri kaydeder (SaveChangesAsync)
        Task SaveChangesAsync();

        // Şifreyi hash'ler ve güvenli hale getirir
        string HashPassword(string password);
    }
}
