using System.Collections.Generic;
using System.Xml.Serialization;
using System.Web;
using System.IO;
using NPORT.Models;
using Microsoft.AspNet.Identity;

namespace NPORT.Database.XMLDatabase
{
    public static class Users
    {
        private static string Path = HttpContext.Current.Server.MapPath( "/App_Data/UserDatabase.xml" );
        private static XmlSerializer formatter = new XmlSerializer(typeof(List<ApplicationUser>));

        public static List<ApplicationUser> GetList()
        {

            //List<ApplicationUser> users = new List<ApplicationUser>();
            //var user = new ApplicationUser("Bogdan", "123456", "+380930808372", 1);
            //users.Add( user );
            //user = new ApplicationUser( "Vanya", "123456", "+380930808373", 2 );
            //users.Add( user );
            //user = new ApplicationUser( "Vanya", "123456", "+380930808374", 3 );
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
