using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using System.Web;
using System.Security.Cryptography;
using NPORT.Models.Database;

namespace NPORT.Database.XMLDatabase
{
    public static class Roles
    {
        private static string Path = HttpContext.Current.Server.MapPath( "/App_Data/RoleDatabase.xml" );
        private static XmlSerializer formatter = new XmlSerializer(typeof(List<Role>));

        public static List<Role> GetList()
        {

            //List<Role> roles = new List<Role>();
            //var role = new Role { Id = 1, Name = "Admin", Access_AddNews = true, Access_EditNews = true, Access_RemoveNews = true };
            //roles.Add( role );
            //role = new Role { Id = 2, Name = "Editor", Access_AddNews = false, Access_EditNews = true, Access_RemoveNews = true };
            //roles.Add( role );
            //role = new Role { Id = 3, Name = "Correspondent", Access_AddNews = true, Access_EditNews = false, Access_RemoveNews = false };
            //roles.Add( role );
            //role = new Role { Id = 4, Name = "User", Access_AddNews = false, Access_EditNews = false, Access_RemoveNews = false };
            //roles.Add( role );
            //Update( roles );
            using (StreamReader fs = new StreamReader( Path ))
            {
                var result = (List<Role>)formatter.Deserialize( fs );
                fs.Close();
                return result;
            }
        }

        public static Role Find( int Id )
        {
            var roles = GetList();
            foreach (var role in roles)
                if (role.Id == Id)
                    return role;
            return null;
        }

        public static void Update( List<Role> roles )
        {
            using (StreamWriter fs = new StreamWriter( Path ))
            {
                formatter.Serialize( fs, roles );
                fs.Close();
            }
        }
    }
}
