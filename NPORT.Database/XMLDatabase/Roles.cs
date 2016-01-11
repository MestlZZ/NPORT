using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Web;
using System.Security.Cryptography;
using NPORT.Models.Database;

namespace NPORT.Database.XMLDatabase
{
    public static class Roles
    {
        private static XmlDocument document;
        private static string Path = HttpContext.Current.Server.MapPath( "/App_Data/RoleDatabse.xml" );

        private static void OpenDatabase()
        {
            document = new XmlDocument();
            document.Load( Path );
        }

        public static List<Role> GetList()
        {
            OpenDatabase();

            List<Role> roles = new List<Role>();

            foreach (XmlNode note in document.SelectNodes( "RoleList/Role" ))
            {
                Role role = new Role();
                role.Id = Convert.ToInt32( note.Attributes["Id"].Value );
                role.Name = Convert.ToString( note.SelectSingleNode( "Name" ).Attributes["value"].Value );
                var access = document.SelectSingleNode("Accesses");
                role.Access_RemoveNews = Convert.ToBoolean(access.SelectSingleNode( "RemoveNews" ).Attributes["value"].Value);
                role.Access_AddNews = Convert.ToBoolean( access.SelectSingleNode( "AddNews" ).Attributes["value"].Value );
                role.Access_EditNews = Convert.ToBoolean( access.SelectSingleNode( "EditNews" ).Attributes["value"].Value );
                roles.Add( role );
            }

            return roles;
        }

        public static Role Find( string Id )
        {
            OpenDatabase();

            Role role = null;

            foreach (XmlNode note in document.SelectNodes( "RoleList/Role" ))
            {
                if (Id == Convert.ToString( note.Attributes["Id"].Value ))
                {
                    role = new Role();
                    role.Id = Convert.ToInt32( note.Attributes["Id"].Value );
                    role.Name = Convert.ToString( note.SelectSingleNode( "Name" ).Attributes["value"].Value );
                    var access = document.SelectSingleNode("Accesses");
                    role.Access_RemoveNews = Convert.ToBoolean( access.SelectSingleNode( "RemoveNews" ).Attributes["value"].Value );
                    role.Access_AddNews = Convert.ToBoolean( access.SelectSingleNode( "AddNews" ).Attributes["value"].Value );
                    role.Access_EditNews = Convert.ToBoolean( access.SelectSingleNode( "EditNews" ).Attributes["value"].Value );
                    break;
                }
            }

            return role;
        }

        public static int LastId()
        {
            OpenDatabase();
            int id = 0;
            foreach (XmlNode note in document.SelectNodes( "RoleList/Role" ))
            {
                id = Convert.ToInt32(note.Attributes["Id"].Value);
            }
            return id;
        }

        public static void Register( Role role )
        {
            OpenDatabase();
            role.Id = LastId() + 1;

            XmlElement newRole = document.CreateElement("Role");
            newRole.SetAttribute( "Id", role.Id.ToString() );

            XmlElement element = document.CreateElement("Name");
            element.SetAttribute( "value", role.Name );
            newRole.AppendChild( element );

            XmlElement access = document.CreateElement("Accesses");

            element = document.CreateElement("RemoveNews");
            element.SetAttribute("value", role.Access_RemoveNews.ToString());
            access.AppendChild( element );

            element = document.CreateElement( "AddNews" );
            element.SetAttribute( "value", role.Access_AddNews.ToString() );
            access.AppendChild( element );

            element = document.CreateElement( "EditNews" );
            element.SetAttribute( "value", role.Access_EditNews.ToString() );
            access.AppendChild( element );

            newRole.AppendChild( access );

            document.DocumentElement.AppendChild( newRole );
            document.Save( Path );
        }
    }
}
