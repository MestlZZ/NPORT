using Microsoft.AspNet.Identity;
using System.Threading.Tasks;
using System;
using System.Security.Claims;
using NPORT.Models.Identity;
using NPORT.Identity;
using NPORT.Database.XMLDatabase;

namespace NPORT.Models.Identity
{
    public class ApplicationUser : IUser<string>
    {
        public ApplicationUser()
        { }

        public ApplicationUser(string name, string password, string phone, string roleId)
        {
            Id = Guid.NewGuid().ToString();

            UserName = name;

            CustomPasswordHasher hasher = new CustomPasswordHasher();
            PasswordHash = hasher.HashPassword(password);

            Phone = phone;

            RoleId = roleId;
        }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(CustomUserManager manager)
        {
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);

            userIdentity.AddClaim(new Claim(ClaimTypes.MobilePhone, Phone));

            return userIdentity;
        }

        public string GetRoleName()
        {
            var role = RoleDb.Find(RoleId);

            return role.Name;
        }

        public string Id { get; set; }

        public string UserName { get; set; }

        public string PasswordHash { get; set; }

        public string Phone { get; set; }

        public string RoleId { get; set; }
    }
}