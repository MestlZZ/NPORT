//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Xml;
//using System.Web;
//using System.Security.Cryptography;
//using NPORT.Models.Database;

//namespace NPORT.Database.XMLDatabase
//{
//    public static class Roles
//    {
//        private static XmlDocument document;
//        private static string Path = HttpContext.Current.Server.MapPath( "/App_Data/RoleDatabse.xml" );

//        private static void OpenDatabase()
//        {
//            document = new XmlDocument();
//            document.Load( Path );
//        }

//        public static List<Role> GetList()
//        {
//            OpenDatabase();

//            List<Role> users = new List<Role>();

//            foreach (XmlNode note in document.SelectNodes( "RoleList/Role" ))
//            {
//                User user = new User();
//                user.Id = Convert.ToString( note.Attributes["Id"].Value );
//                user.Login = Convert.ToString( note.SelectSingleNode( "Login" ).Attributes["value"].Value );
//                user.Password = Convert.ToString( note.SelectSingleNode( "Password" ).Attributes["value"].Value );
//                user.Nickname = Convert.ToString( note.SelectSingleNode( "Nickname" ).Attributes["value"].Value );
//                user.Phone = Convert.ToString( note.SelectSingleNode( "Phone" ).Attributes["value"].Value );
//                user.Mail = Convert.ToString( note.SelectSingleNode( "Mail" ).Attributes["value"].Value );
//                user.RegisterTime = Convert.ToString( note.SelectSingleNode( "RegisterDate" ).Attributes["value"].Value );
//                user.UserRoleId = Convert.ToInt32( note.SelectSingleNode( "RoleId" ).Attributes["Id"].Value );
//                user.Gender = Convert.ToBoolean( note.SelectSingleNode( "Gender" ).Attributes["value"].Value );
//                users.Add( user );
//            }

//            return users;
//        }

//        public static User Find( string Id )
//        {
//            OpenDatabase();

//            User user = null;

//            foreach (XmlNode note in document.SelectNodes( "UserList/User" ))
//            {
//                if (Id == Convert.ToString( note.Attributes["Id"].Value ))
//                {
//                    user = new User();
//                    user.Id = Convert.ToString( note.Attributes["Id"].Value );
//                    user.Login = Convert.ToString( note.SelectSingleNode( "Login" ).Attributes["value"].Value );
//                    user.Password = Convert.ToString( note.SelectSingleNode( "Password" ).Attributes["value"].Value );
//                    user.Nickname = Convert.ToString( note.SelectSingleNode( "Nickname" ).Attributes["value"].Value );
//                    user.Phone = Convert.ToString( note.SelectSingleNode( "Phone" ).Attributes["value"].Value );
//                    user.Mail = Convert.ToString( note.SelectSingleNode( "Mail" ).Attributes["value"].Value );
//                    user.RegisterTime = Convert.ToString( note.SelectSingleNode( "RegisterDate" ).Attributes["value"].Value );
//                    user.UserRoleId = Convert.ToInt32( note.SelectSingleNode( "RoleId" ).Attributes["Id"].Value );
//                    user.Gender = Convert.ToBoolean( note.SelectSingleNode( "Gender" ).Attributes["value"].Value );
//                    break;
//                }
//            }

//            return user;
//        }

//        public static void Register( User user )
//        {
//            OpenDatabase();
//            user.Id = Guid.NewGuid().ToString();

//            XmlElement newUser = document.CreateElement("User");
//            newUser.SetAttribute( "Id", user.Id );

//            XmlElement element = document.CreateElement("Login");
//            element.SetAttribute( "value", user.Login );
//            newUser.AppendChild( element );

//            var md = MD5.Create();
//            Encoding u8 = Encoding.UTF8;

//            byte[] buff = u8.GetBytes(user.Password + user.Login);
//            buff = md.ComputeHash( buff );

//            char[] chars = new char[buff.Length / sizeof(char)];
//            System.Buffer.BlockCopy( buff, 0, chars, 0, buff.Length );

//            user.Password = new string( chars );

//            element = document.CreateElement( "Password" );
//            element.SetAttribute( "value", user.Password );
//            newUser.AppendChild( element );

//            element = document.CreateElement( "Nickname" );
//            element.SetAttribute( "value", user.Nickname );
//            newUser.AppendChild( element );

//            element = document.CreateElement( "Phone" );
//            element.SetAttribute( "value", user.Phone );
//            newUser.AppendChild( element );

//            element = document.CreateElement( "Mail" );
//            element.SetAttribute( "value", user.Mail );
//            newUser.AppendChild( element );

//            element = document.CreateElement( "RegisterDate" );
//            element.SetAttribute( "value", user.RegisterTime );
//            newUser.AppendChild( element );

//            element = document.CreateElement( "RoleId" );
//            element.SetAttribute( "Id", user.UserRoleId.ToString() );
//            newUser.AppendChild( element );

//            element = document.CreateElement( "Gender" );
//            element.SetAttribute( "value", user.Gender.ToString() );
//            newUser.AppendChild( element );

//            document.DocumentElement.AppendChild( newUser );
//            document.Save( Path );
//        }
//    }
//}
