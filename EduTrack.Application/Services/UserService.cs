using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EduTrack.Application.Interfaces;
using EduTrack.Domain;
using System.Security.Cryptography;
using EduTrack.Data.Repositories.Interfaces;

namespace EduTrack.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task SaveChangesAsync()
        {
            await _userRepository.SaveChangesAsync();
        }

        public string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes(password);
            var hash = sha256.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }

        public async Task AddUserAsync(User user)
        {
            // Şifreyi hash'le
            user.Password = HashPassword(user.Password);

            // Repository'ye ekle
            await _userRepository.AddUserAsync(user);

            // ÖNEMLİ: Burada SaveChanges çağırma, Controller'da çağır
            // await _userRepository.SaveChangesAsync(); // BUNU KALDIR
        }

        public async Task<User?> GetUserByTCAsync(string tcNo)
        {
            return await _userRepository.GetUserByTcNoAsync(tcNo);
        }

        public async Task<IEnumerable<User>> GetUsersByRoleAsync(string role)
        {
            return await _userRepository.GetUsersByRoleAsync(role);
        }

        public async Task<bool> CheckPasswordAsync(string tcNo, string password)
        {
            var user = await GetUserByTCAsync(tcNo);
            if (user == null) return false;

            return user.Password == HashPassword(password);
        }
    }
}