using System.Collections.Generic;
using System.Xml.Serialization;
using System.Web;
using System.IO;

namespace NPORT.Database.XMLDatabase
{
    public static class UsersDb
    {
        private static string Path = HttpContext.Current.Server.MapPath( "/App_Data/UserDatabase.xml" );
        private static XmlSerializer formatter = new XmlSerializer(typeof(List<ApplicationUser>));

        public static List<ApplicationUser> GetList()
        {
            //List<ApplicationRole> roles = new List<ApplicationRole>();
            //ApplicationRole rol = new ApplicationRole("Admin");
            //roles.Add( rol );
            //rol = new ApplicationRole( "Editor" );
            //roles.Add( rol );
            //rol = new ApplicationRole( "Correspondent" );
            //roles.Add( rol );
            //rol = new ApplicationRole( "User" );
            //roles.Add( rol );
            //RoleDb.Update( roles );

            //List<ApplicationUser> users = new List<ApplicationUser>();
            //var user = new ApplicationUser("Admin", "123456", "+380930808372", "");
            //users.Add( user );
            //Update( users );

            using (StreamReader fs = new StreamReader( Path ))
            {
                var result = (List<ApplicationUser>)formatter.Deserialize( fs );
                fs.Close();
                return result;
            }
        }

        public static ApplicationUser Find( string Id )
        {
            var users = GetList();
            foreach (var user in users)
                if (user.Id == Id)
                    return user;
            return null;
        }

        public static ApplicationUser FindLogin( string phone )
        {
            var users = GetList();
            foreach (var user in users)
                if (user.Phone == phone)
                    return user;
            return null;
        }

        public static ApplicationUser FindNickname( string nickname )
        {
            var users = GetList();
            foreach (var user in users)
                if (user.UserName == nickname)
                    return user;
            return null;
        }

        public static void Update( List<ApplicationUser> users )
        {
            using (StreamWriter fs = new StreamWriter( Path ))
            {
                formatter.Serialize( fs, users );
                fs.Close();
            }
        }
    }
}
