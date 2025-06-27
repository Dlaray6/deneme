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

        Task<bool> HasAdminForSchoolAsync(int? schoolId);


        // Veritabanındaki değişiklikleri kaydeder (SaveChangesAsync)
        Task SaveChangesAsync();

        
    }
}
