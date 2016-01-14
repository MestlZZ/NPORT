using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using NPORT.Models;

namespace NPORT.Models
{
    public class CustomUserStore : IUserStore<ApplicationUser>, IUserPasswordStore<ApplicationUser>
    {
        private List<ApplicationUser> Users = NPORT.Database.XMLDatabase.Users.GetList();

        public CustomUserStore()
        {
        }

        public void Dispose()
        {
        }

        public Task SetPasswordHashAsync( ApplicationUser user, string passwordHash )
        {
            user.PasswordHash = passwordHash;
            return Task.FromResult( 0 );
        }
        public Task<string> GetPasswordHashAsync( ApplicationUser user )
        {
            return Task.FromResult<string>( user.PasswordHash );
        }
        public Task<bool> HasPasswordAsync( ApplicationUser user )
        {
            return Task.FromResult<bool>( !string.IsNullOrEmpty( user.PasswordHash ) );
        }

        private void UpdateDb()
        {
            NPORT.Database.XMLDatabase.Users.Update( Users );
        }

        public Task CreateAsync( ApplicationUser user )
        {
            Users.Add( user );
            UpdateDb();
            return Task.Factory.StartNew( () => Users.Add( user ) );
        }

        public Task UpdateAsync( ApplicationUser user )
        {
            var task = FindByIdAsync( user.Id );
            task.Start();
            DeleteAsync( task.Result );
            CreateAsync(user);
            UpdateDb();

            return Task.Factory.StartNew( () => CreateAsync( task.Result ) );
        }

        public Task DeleteAsync( ApplicationUser user )
        {
            Users.Remove( user );
            UpdateDb();
            return Task.Factory.StartNew( () => Users.Remove( user ) );
        }

        public Task<ApplicationUser> FindByIdAsync( string userId )
        {
            return Task<ApplicationUser>.Factory.StartNew( () => Users.FirstOrDefault( u => u.Id == userId ) );
        }

        public Task<ApplicationUser> FindByNameAsync( string userName )
        {
            var user = Users.FirstOrDefault( u => u.Phone == userName );
            return Task<ApplicationUser>.Factory.StartNew( () => Users.FirstOrDefault( u => u.Phone == userName ) );
        }
    }
}