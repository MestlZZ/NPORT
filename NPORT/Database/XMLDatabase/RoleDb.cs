using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using System.Web;
using NPORT.Models.Identity;
using NPORT.Database.Interfaces;

namespace NPORT.Database.XMLDatabase
{
    public class RoleDb : IRoleDatabase<ApplicationRole, string>
    {
        private string Path = HttpContext.Current.Server.MapPath("/App_Data/RoleDatabase.xml");

        private XmlSerializer formatter = new XmlSerializer(typeof(List<ApplicationRole>));

        public List<ApplicationRole> GetList()
        {
            List<ApplicationRole> roleList;

            using (StreamReader fileWithRole = new StreamReader(Path))
            {
                roleList = (List<ApplicationRole>)formatter.Deserialize(fileWithRole);
            }

            return roleList;
        }

        public ApplicationRole Find(string id)
        {
            var roleList = GetList();

            foreach (var role in roleList)
                if (role.Id == id)
                    return role;

            return null;
        }
        public ApplicationRole FindByName(string name)
        {
            var roleList = GetList();

            foreach (var role in roleList)
                if (role.Name == name)
                    return role;
            return null;
        }
    }
}
