﻿using Microsoft.AspNet.Identity;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Text;
using System;
using System.Security.Claims;

namespace NPORT
{
    public class ApplicationUser : IUser<string>
    {
        public ApplicationUser()
        { }

        public ApplicationUser( string name, string password, string phone, int role )
        {
            Id = Guid.NewGuid().ToString();
            UserName = name;

            var md = MD5.Create();
            Encoding u8 = Encoding.UTF8;

            byte[] buff = u8.GetBytes(password);
            buff = md.ComputeHash( buff );

            char[] chars = new char[buff.Length / sizeof(char)];
            Buffer.BlockCopy( buff, 0, chars, 0, buff.Length );

            PasswordHash = new string( chars );

            Phone = phone;

            Role = role;
        }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync( CustomUserManager manager )
        {
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            userIdentity.AddClaim( new Claim( ClaimTypes.MobilePhone, this.Phone ) );
            return userIdentity;
        }

        public string GetRole()
        {
            var roles = Database.XMLDatabase.Roles.Find(Role);
            return roles.Name;
        }

        public string Id { get; set; }

        public string UserName { get; set; }

        public string PasswordHash { get; set; }

        public string Phone { get; set; }

        public int Role { get; set; }
    }
}