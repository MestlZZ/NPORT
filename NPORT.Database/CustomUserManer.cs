using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using System.Web;
using System.IO;
using System.Security.Principal;
using System.Security.Cryptography;
using NPORT.Models.Database;
using NPORT.Models;
using Microsoft.AspNet.Identity;

namespace NPORT
{
    public class CustomUserManager : UserManager<ApplicationUser, string>
    {
        public CustomUserManager( CustomUserStore store )
            : base( store )
        {
            this.PasswordHasher = new CustomPasswordHasher();
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

    public class CustomPasswordHasher : PasswordHasher
    {
        public override string HashPassword( string password )
        {
            var md = MD5.Create();
            Encoding u8 = Encoding.UTF8;

            byte[] buff = u8.GetBytes(password);
            buff = md.ComputeHash( buff );

            char[] chars = new char[buff.Length / sizeof(char)];
            System.Buffer.BlockCopy( buff, 0, chars, 0, buff.Length );

            password = new string(chars);
            return password;
        }

        public override PasswordVerificationResult VerifyHashedPassword( string hashedPassword, string providedPassword )
        {
            if ( hashedPassword == HashPassword(providedPassword) )
            {
                return PasswordVerificationResult.SuccessRehashNeeded;
            }
            else
            {
                return PasswordVerificationResult.Failed;
            }
        }
    }
}
