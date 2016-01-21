using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;

namespace NPORT
{
    public class ApplicationRole : IRole<string>
    {
        public ApplicationRole()
        {
            Id = Guid.NewGuid().ToString();
        }

        public ApplicationRole( string name )
            : this()
        {
            Name = name;
        }

        public string Id { get; set; }

        public string Name { get; set; }
    }
}