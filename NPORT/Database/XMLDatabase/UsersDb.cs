using System.Collections.Generic;
using System.Xml.Serialization;
using System.Web;
using System.IO;
using NPORT.Models.Identity;
using NPORT.Database.Interfaces;
using System.Linq;

namespace NPORT.Database.XMLDatabase
{
    public class UsersDb : IUserDatabase<ApplicationUser, string>
    {
        private static string Path = HttpContext.Current.Server.MapPath("/App_Data/UserDatabase.xml");

        private XmlSerializer formatter = new XmlSerializer(typeof(List<ApplicationUser>));
                     

        public List<ApplicationUser> GetList()
        {
            

            using (var fileWithUsers = new StreamReader(Path))
            {
               return (List<ApplicationUser>)formatter.Deserialize(fileWithUsers);
            }

            
        }
               
        public ApplicationUser Find(string id)
        {
            return GetList().FirstOrDefault(u => u.Id == id);                     
        }

        public ApplicationUser FindByLogin(string phone)
        {
            var users = GetList();

            foreach (var user in users)
                if (user.Phone == phone)
                    return user;

            return null;
        }

        public ApplicationUser FindByUsername(string nickname)
        {
            var users = GetList();

            foreach (var user in users)
                if (user.UserName == nickname)
                    return user;

            return null;
        }

        public void Update(List<ApplicationUser> users)
        {
            using (StreamWriter fileWithUsers = new StreamWriter(Path))
            {
                formatter.Serialize(fileWithUsers, users);
            }
        }
    }
}
