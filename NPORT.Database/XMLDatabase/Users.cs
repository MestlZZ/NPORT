﻿using System.Collections.Generic;
using System.Xml.Serialization;
using System.Web;
using System.IO;
using NPORT.Models;
using Microsoft.AspNet.Identity;

namespace NPORT.Database.XMLDatabase
{
    public static class Users
    {
        private static string Path = HttpContext.Current.Server.MapPath( "/App_Data/UserDatabse.xml" );
        private static XmlSerializer formatter = new XmlSerializer(typeof(List<ApplicationUser>));

        public static List<ApplicationUser> GetList()
        {
            
            //List<ApplicationUser> users = new List<ApplicationUser>();
            //var user = new ApplicationUser("Bogdan", "123", "+380930808372");
            //users.Add( user );
            //user = new ApplicationUser("Vanya", "123", "+380930808370");
            //users.Add( user );
            //Update( users );
            using (StreamReader fs = new StreamReader( Path ))
            {
                var result = (List<ApplicationUser>)formatter.Deserialize( fs );
                fs.Close();
                return result;
            }
        }

        //public static ApplicationUser Find( string username )
        //{
        //    var users = GetList();

        //    foreach(var user in users)
        //    {
        //        if (user.UserName == username)
        //            return user;
        //    }
        //    return null;
        //}

        //public static ApplicationUser Find( string login, string password )
        //{
        //    var users = GetList();

        //    foreach (var user in users)
        //    {
        //        if (user.UserName == login && user.PasswordHash == HashPassword(password))
        //            return user;
        //    }
        //    return null;
        //}

        //public static string HashPassword( string password )
        //{
        //    PasswordHasher s = new PasswordHasher();
        //    return s.HashPassword( password );
        //}

        //public static void Register( ApplicationUser user )
        //{
        //    var users = GetList();

        //    users.Add( user );

        //    Update( users );            
        //}

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
