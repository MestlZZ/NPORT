using System.Collections.Generic;
using System.Xml.Serialization;
using System.Web;
using System.IO;
using NPORT.Models.Identity;

namespace NPORT.Database.XMLDatabase
{
    public static class UsersDb
    {
        private static string Path = HttpContext.Current.Server.MapPath("/App_Data/UserDatabase.xml");

        private static XmlSerializer formatter = new XmlSerializer(typeof(List<ApplicationUser>));

        public static List<ApplicationUser> GetList()
        {
            List<ApplicationUser> result;

            using (StreamReader fileWithUsers = new StreamReader(Path))
            {
                result = (List<ApplicationUser>)formatter.Deserialize(fileWithUsers);
            }

            return result;
        }

        public static ApplicationUser Find(string Id)
        {
            var users = GetList();

            foreach (var user in users)
                if (user.Id == Id)
                    return user;

            return null;
        }

        public static ApplicationUser FindByLogin(string phone)
        {
            var users = GetList();

            foreach (var user in users)
                if (user.Phone == phone)
                    return user;

            return null;
        }

        public static ApplicationUser FindByUsername(string nickname)
        {
            var users = GetList();

            foreach (var user in users)
                if (user.UserName == nickname)
                    return user;

            return null;
        }

        public static void Update(List<ApplicationUser> users)
        {
            using (StreamWriter fileWithUsers = new StreamWriter(Path))
            {
                formatter.Serialize(fileWithUsers, users);
            }
        }
    }
}
