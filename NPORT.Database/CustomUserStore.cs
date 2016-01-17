using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

namespace NPORT.Models
{
    public class CustomUserStore : IUserStore<ApplicationUser>, IUserPasswordStore<ApplicationUser>, IUserLockoutStore<ApplicationUser, string>, IUserTwoFactorStore<ApplicationUser, string>
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

        public void Create( ApplicationUser user )
        {
            Users.Add( user );
            UpdateDb();
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

        public void Update( ApplicationUser user )
        {
            var task = FindById( user.Id );
            Delete( task );
            Create( user );
            Users.Sort( delegate(ApplicationUser u1, ApplicationUser u2) { if (u1.Role > u2.Role) { return 1; } else { if (String.Compare( u1.UserName , u2.UserName ) > 0) { return 1; } return -1; } } );
            UpdateDb();
        }

        public Task DeleteAsync( ApplicationUser user )
        {
            Users.Remove( user );
            UpdateDb();
            return Task.Factory.StartNew( () => Users.Remove( user ) );
        }

        public void Delete( ApplicationUser user )
        {
            Users.Remove( user );
            var comments = NPORT.Database.JSONDatabase.CommentsJson.GetList();
            foreach (var comment in comments)
            {
                if (comment.AuthorId == user.Id)
                {
                    NPORT.Database.JSONDatabase.CommentsJson.Remove( comment.Id );
                }
            }
            UpdateDb();
        }

        public ApplicationUser FindById( string userId )
        {
            return Users.FirstOrDefault( u => u.Id == userId );
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

        public Task<DateTimeOffset> GetLockoutEndDateAsync( ApplicationUser user )
        {
            DateTime date = new DateTime(2000, 03, 23);
            return Task<DateTimeOffset>.Factory.StartNew( () => date );
        }

        public Task SetLockoutEndDateAsync( ApplicationUser user, DateTimeOffset lockoutEnd )
        {
            return Task.Factory.StartNew( () => Nothing() );
        }

        private void Nothing()
        { }


        public Task<int> IncrementAccessFailedCountAsync( ApplicationUser user )
        {
            int s = 1;
            return Task<int>.Factory.StartNew( () => s );
        }

        public Task ResetAccessFailedCountAsync( ApplicationUser user )
        {
            return Task.Factory.StartNew( () => Nothing() );
        }

        public Task<int> GetAccessFailedCountAsync( ApplicationUser user )
        {
            int s = 0;
            return Task<int>.Factory.StartNew( () => s );
        }

        public Task<bool> GetLockoutEnabledAsync( ApplicationUser user )
        {
            bool s = false;
            return Task<bool>.Factory.StartNew( () => s );
        }

        public Task SetLockoutEnabledAsync( ApplicationUser user, bool enabled )
        {
            return Task.Factory.StartNew( () => Nothing() );
        }

        public Task SetTwoFactorEnabledAsync( ApplicationUser user, bool enabled )
        {
            return Task.Factory.StartNew( () => Nothing() );
        }

        public Task<bool> GetTwoFactorEnabledAsync( ApplicationUser user )
        {
            bool s = false;
            return Task<bool>.Factory.StartNew( () => s );
        }
    }
}