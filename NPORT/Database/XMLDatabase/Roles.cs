using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using System.Web;
using NPORT.Models.Database;

namespace NPORT.Database.XMLDatabase
{
    public static class Roles
    {
        private static string Path = HttpContext.Current.Server.MapPath( "/App_Data/RoleDatabase.xml" );
        private static XmlSerializer formatter = new XmlSerializer(typeof(List<Role>));

        public static List<Role> GetList()
        {
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
