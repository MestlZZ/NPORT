using System.Collections.Generic;
using System.Xml.Serialization;
using System.Web;
using System.IO;

namespace NPORT.Database.XMLDatabase
{
    public static class Users
    {
        private static string Path = HttpContext.Current.Server.MapPath( "/App_Data/UserDatabase.xml" );
        private static XmlSerializer formatter = new XmlSerializer(typeof(List<ApplicationUser>));

        public static List<ApplicationUser> GetList()
        {
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
        public static List<Models.Database.ApplicationUser> ConvertAndGetUsers()
        {
            var users = GetList();
            List<Models.Database.ApplicationUser> newlist = new List<Models.Database.ApplicationUser>();
            foreach (var user in users)
            {
                newlist.Add(user.ConvertUser());
            }
            return newlist;
        }
    }
}
