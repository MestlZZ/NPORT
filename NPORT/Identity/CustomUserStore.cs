using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using NPORT.Models.Identity;
using NPORT.Identity;

namespace NPORT.Identity
{
    public class CustomUserStore : IUserStore<ApplicationUser>, IUserPasswordStore<ApplicationUser>, IUserLockoutStore<ApplicationUser, string>, IUserTwoFactorStore<ApplicationUser, string>
    {
        private List<ApplicationUser> Users = NPORT.Database.XMLDatabase.UsersDb.GetList();

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
            NPORT.Database.XMLDatabase.UsersDb.Update( Users );
        }

        public void Create( ApplicationUser user )
        {
            Users.Add( user );
            UpdateDb();
        }

        public void Update( ApplicationUser user )
        {
            var task = FindById( user.Id );

            Delete( task );

            Create( user );

            Users.Sort( 
                delegate(ApplicationUser u1, ApplicationUser u2) 
                {
                    if (String.Compare( u1.UserName , u2.UserName ) > 0)
                        return 1;
                    else
                        return -1;
                }  
            );

            UpdateDb();
        }

        public void Delete( ApplicationUser user )
        {
            Users.Remove( user );
            var comments = NPORT.Database.JSONDatabase.CommentsDb.GetList();
            foreach (var comment in comments)
            {
                if (comment.AuthorId == user.Id)
                {
                    NPORT.Database.JSONDatabase.CommentsDb.Remove( comment.Id );
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
            return Task<ApplicationUser>.Factory.StartNew( () => Users.FirstOrDefault( u => u.Phone == userName ) );
        }

        public Task CreateAsync( ApplicationUser user )
        {
            return Task.Run( () => Create( user ) );
        }

        public Task UpdateAsync( ApplicationUser user )
        {
            return Task.Run( () => Update( user ) );
        }

        public Task DeleteAsync( ApplicationUser user )
        {
            return Task.Run( () => Delete( user ) );
        }

        public Task<DateTimeOffset> GetLockoutEndDateAsync( ApplicationUser user )
        {
            return Task.FromResult( DateTimeOffset.UtcNow );
        }

        public Task SetLockoutEndDateAsync( ApplicationUser user, DateTimeOffset lockoutEnd )
        {
            return Task.Run( () => { } );
        }

        public Task<int> IncrementAccessFailedCountAsync( ApplicationUser user )
        {
            return Task.FromResult( 1 );
        }

        public Task ResetAccessFailedCountAsync( ApplicationUser user )
        {
            return Task.Run( () => { } );
        }

        public Task<int> GetAccessFailedCountAsync( ApplicationUser user )
        {
            return Task.FromResult( 0 );
        }

        public Task<bool> GetLockoutEnabledAsync( ApplicationUser user )
        {
            return Task.FromResult(false);
        }

        public Task SetLockoutEnabledAsync( ApplicationUser user, bool enabled )
        {
            return Task.Run( () => { } );
        }

        public Task SetTwoFactorEnabledAsync( ApplicationUser user, bool enabled )
        {
            return Task.Run( () => { } );
        }

        public Task<bool> GetTwoFactorEnabledAsync( ApplicationUser user )
        {
            return Task.FromResult( false );
        }
    }
}