using Microsoft.AspNet.Identity;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text;
using System;
using System.Security.Claims;

namespace NPORT
{
    public class ApplicationUser : IUser<string>
    {
        public ApplicationUser()
        { }

        public ApplicationUser( string name, string password, string phone, string roleId )
        {
            Id = Guid.NewGuid().ToString();
            UserName = name;

            CustomPasswordHasher hasher= new CustomPasswordHasher();
            PasswordHash = hasher.HashPassword(password);

            Phone = phone;

            RoleId = roleId;
        }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync( CustomUserManager manager )
        {
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            userIdentity.AddClaim( new Claim( ClaimTypes.MobilePhone, Phone ) );
            return userIdentity;
        }

        public string GetRoleName()
        {
            var role = Database.XMLDatabase.RoleDb.Find(RoleId);
            return role.Name;
        }

        public ApplicationRole GetRole()
        {
            var role = Database.XMLDatabase.RoleDb.Find(RoleId);
            return role;
        }

        public string Id { get; set; }

        public string UserName { get; set; }

        public string PasswordHash { get; set; }

        public string Phone { get; set; }

        public string RoleId { get; set; }
    }
}