using System;
using Microsoft.AspNet.Identity;

namespace NPORT.Models.Identity
{
    public class ApplicationRole : IRole<string>
    {
        public ApplicationRole()
        {
            Id = Guid.NewGuid().ToString();
        }

        public ApplicationRole(string name)
            : this()
        {
            Name = name;
        }

        public string Id { get; set; }

        public string Name { get; set; }
    }
}