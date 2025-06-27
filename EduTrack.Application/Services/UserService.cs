using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;
using System.Threading.Tasks;
using EduTrack.Application.Interfaces;
using EduTrack.Data.Repositories.Interfaces;
using EduTrack.Domain;

namespace EduTrack.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task AddUserAsync(User user)
        {
      
            await _userRepository.AddUserAsync(user);
        }

        public async Task<User?> GetUserByTCAsync(string tc)
        {
            return await _userRepository.GetUserByTcNoAsync(tc);
        }

        public async Task<IEnumerable<User>> GetUsersByRoleAsync(string role)
        {
            return await _userRepository.GetUsersByRoleAsync(role);
        }


        public async Task<bool> HasAdminForSchoolAsync(int? schoolId)
        {
            return await _userRepository.HasAdminForSchoolAsync(schoolId);
        }

        public async Task SaveChangesAsync()
        {
            await _userRepository.SaveChangesAsync();
        }

     
    }
}
