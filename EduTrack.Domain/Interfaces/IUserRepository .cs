using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using EduTrack.Domain;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace EduTrack.Data.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task AddUserAsync(User user);
        Task<User?> GetUserByTcNoAsync(string tcNo);
        Task<IEnumerable<User>> GetUsersByRoleAsync(string role);
        Task SaveChangesAsync();
    }
}


