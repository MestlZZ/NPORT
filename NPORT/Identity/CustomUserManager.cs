using System.Threading.Tasks;
using System.Web;
using NPORT.Models;
using Microsoft.AspNet.Identity;

namespace NPORT
{
    public class CustomUserManager : UserManager<ApplicationUser, string>
    {
        public CustomUserManager( CustomUserStore store )
            : base( store )
        {
            PasswordHasher = new CustomPasswordHasher();
        }

        public static CustomUserManager Create()
        {
            var manager = new CustomUserManager(new CustomUserStore());
            return manager;
        }

        public Task<ApplicationUser> FindAsync( string userName, string password, HttpRequest request, HttpResponse responce )
        {
            Task<ApplicationUser> taskInvoke = Task<ApplicationUser>.Factory.StartNew(() =>
            {
                var user = Store.FindByNameAsync(userName).Result;
                if (user.PasswordHash == PasswordHasher.HashPassword(password))
                {
                    return user;
                }
                return null;
            });
            return taskInvoke;
        }
    }
}
