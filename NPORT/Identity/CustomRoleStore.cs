using System;
using System.Collections.Generic;
using System.Linq;
using NPORT.Database.XMLDatabase;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

namespace NPORT
{
    public class CustomRoleStore : IRoleStore<ApplicationRole, string>
    {
        private List<ApplicationRole> RoleList = RoleDb.GetList();

        public CustomRoleStore()
        {
        }
        
        public void Dispose()
        {
        }

        private void UpdateDb()
        {
            RoleDb.Update( RoleList );
        }

        public void Create( ApplicationRole role )
        {
            RoleList.Add( role );
            UpdateDb();
        }

        public Task CreateAsync( ApplicationRole role )
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync( ApplicationRole role )
        {
            throw new NotImplementedException();
        }

        public Task<ApplicationRole> FindByIdAsync( string roleId )
        {
            return Task<ApplicationRole>.Factory.StartNew( () => RoleList.FirstOrDefault( r => r.Id == roleId ) );
        }

        public Task<ApplicationRole> FindByNameAsync( string roleName )
        {
            return Task<ApplicationRole>.Factory.StartNew( () => RoleList.FirstOrDefault( r => r.Name == roleName ) );
        }

        public Task UpdateAsync( ApplicationRole role )
        {
            throw new NotImplementedException();
        }
    }
}