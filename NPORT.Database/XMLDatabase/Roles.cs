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
                var access = note.SelectSingleNode("Accesses");
                role.Access_RemoveNews = Convert.ToBoolean( access.SelectSingleNode( "RemoveNews" ).Attributes["value"].Value );
                role.Access_AddNews = Convert.ToBoolean( access.SelectSingleNode( "AddNews" ).Attributes["value"].Value );
                role.Access_EditNews = Convert.ToBoolean( access.SelectSingleNode( "EditNews" ).Attributes["value"].Value );
                roles.Add( role );
            }

            return roles;
        }

        public static Role Find( int Id )
        {
            OpenDatabase();

            Role role = null;

            foreach (XmlNode note in document.SelectNodes( "RoleList/Role" ))
            {
                if (Id == Convert.ToInt32( note.Attributes["Id"].Value ))
                {
                    role = new Role();
                    role.Id = Convert.ToInt32( note.Attributes["Id"].Value );
                    role.Name = Convert.ToString( note.SelectSingleNode( "Name" ).Attributes["value"].Value );
                    var access = note.SelectSingleNode("Accesses");
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

            foreach (XmlNode note in document.SelectNodes( "RoleList/Role" ))
            {
            }

        public static void Register( Role user )
        {
            OpenDatabase();
            user.Id = Guid.NewGuid().ToString();

            XmlElement newUser = document.CreateElement("User");
            newUser.SetAttribute( "Id", user.Id );

            XmlElement element = document.CreateElement("Login");
            element.SetAttribute( "value", user.Login );
            newUser.AppendChild( element );

            var md = MD5.Create();
            Encoding u8 = Encoding.UTF8;

            byte[] buff = u8.GetBytes(user.Password + user.Login);
            buff = md.ComputeHash( buff );

            char[] chars = new char[buff.Length / sizeof(char)];
            System.Buffer.BlockCopy( buff, 0, chars, 0, buff.Length );

            user.Password = new string( chars );

            element = document.CreateElement( "Password" );
            element.SetAttribute( "value", user.Password );
            newUser.AppendChild( element );

            element = document.CreateElement( "Nickname" );
            element.SetAttribute( "value", user.Nickname );
            newUser.AppendChild( element );

            element = document.CreateElement( "Phone" );
            element.SetAttribute( "value", user.Phone );
            newUser.AppendChild( element );

            element = document.CreateElement( "Mail" );
            element.SetAttribute( "value", user.Mail );
            newUser.AppendChild( element );

            element = document.CreateElement( "RegisterDate" );
            element.SetAttribute( "value", user.RegisterTime );
            newUser.AppendChild( element );

            element = document.CreateElement( "RoleId" );
            element.SetAttribute( "Id", user.UserRoleId.ToString() );
            newUser.AppendChild( element );

            element = document.CreateElement( "Gender" );
            element.SetAttribute( "value", user.Gender.ToString() );
            newUser.AppendChild( element );

            document.DocumentElement.AppendChild( newUser );
            document.Save( Path );
        }
    }
}
