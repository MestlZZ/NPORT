using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using System.Web;
using NPORT.Models.Database;
using NPORT.Models.Identity;
using NPORT.Identity;

namespace NPORT.Database.XMLDatabase
{
    public static class RoleDb
    {
        private static string Path = HttpContext.Current.Server.MapPath( "/App_Data/RoleDatabase.xml" );
        private static XmlSerializer formatter = new XmlSerializer(typeof(List<ApplicationRole>));

        public static List<ApplicationRole> GetList()
        {
            using (StreamReader fileWithRole = new StreamReader( Path ))
            {
                var roleList = (List<ApplicationRole>)formatter.Deserialize( fileWithRole );
                fileWithRole.Close();
                return roleList;
            }
        }

        public static ApplicationRole Find( string Id )
        {
            var roleList = GetList();
            foreach (var role in roleList)
                if (role.Id == Id)
                    return role;
            return null;
        }

        public static void Update( List<ApplicationRole> roleList )
        {
            using (StreamWriter fs = new StreamWriter( Path ))
            {
                formatter.Serialize( fs, roleList );
                fs.Close();
            }
        }
    }
}
