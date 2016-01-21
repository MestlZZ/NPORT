using System.Threading.Tasks;
using System.Web;
using NPORT.Models;
using Microsoft.AspNet.Identity;

namespace NPORT
{
    public class CustomRoleManager : RoleManager<ApplicationRole>
    {
        public CustomRoleManager( IRoleStore<ApplicationRole, string> store )
            : base( store )
        {
        }
        public static CustomRoleManager Create()
        {
            var manager = new CustomRoleManager(new CustomRoleStore());
            return manager;
        }

    }
}