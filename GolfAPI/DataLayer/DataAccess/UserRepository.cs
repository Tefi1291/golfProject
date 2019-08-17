using GolfAPI.Core.Contracts.DataAccess;
using GolfAPI.DataLayer.ADL;
using GolfAPI.DataLayer.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GolfAPI.DataLayer.DataAccess
{
    public class UserRepository: BaseRepository, IUserRepository
    {
        public UserRepository(GolfDatabaseContext context) : base(context)
        { }

        public User GetUserByGuid(string guid)
        {
            var id = Guid.Parse(guid);
            return _context.Users.FirstOrDefault(u => u.Guid == id);
        }

        public User GetUserById(int Id)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id.Equals(Id));
            return user;
        }

        public User GetUserByUsername(string username)
        {
            User result = null;
            if (username != null)
            {
                result = _context.Users.FirstOrDefault(u => u.Username == username);

            }
            return result;
        }
    }
}
