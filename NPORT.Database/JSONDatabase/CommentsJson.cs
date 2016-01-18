using System;
using System.Collections.Generic;
using System.Web;
using Newtonsoft.Json;
using System.IO;

namespace NPORT.Database.JSONDatabase
{
    public class CommentsJson
    {
        private static string Path = HttpContext.Current.Server.MapPath( "/App_Data/CommentsDatabase.json" );

        public static void Add( Models.Database.Comment comment )
        {
            comment.Date = DateTime.UtcNow.ToString();
            comment.Id = 1;

            List<Models.Database.Comment> items;

            if (File.Exists( Path ))
            {
                StreamReader file = new StreamReader(Path);

                string json = file.ReadToEnd();

                items = JsonConvert.DeserializeObject<List<Models.Database.Comment>>(json);

                if (items.Count > 0)
                    comment.Id = items[0].Id + 1;
                else
                    items = new List<Models.Database.Comment>();

                file.Close();
            }
            else
            {
                items = new List<Models.Database.Comment>();
            }  

            items.Insert( 0, comment );

            StreamWriter file2 = new StreamWriter(Path);

            file2.Write( JsonConvert.SerializeObject( items ) );

            file2.Close();
        }

        public static List<Models.Database.Comment> GetListForNewsId( string id )
        {
            var comments = GetList();

            var result = new List<Models.Database.Comment>();

            foreach ( var comment in comments )
            {
                if (comment.NewsId == id)
                    result.Add( comment );
            }

            if (result.Count > 0)
                return result;
            else
                return null;
        }

        public static List<string> GetListAuthorNameForComments( List<Models.Database.Comment> comments )
        {
            var names = new List<string>();

            foreach( var comment in comments )
            {
                names.Add(XMLDatabase.Users.Find( comment.AuthorId ).UserName);
            }

            return names;
        }

        public static Models.Database.Comment Find( int id )
        {
            var list = GetList();
            foreach (var comment in list)
                if (comment.Id == id)
                    return comment;
            return null;
        }

        public static List<Models.Database.Comment> GetList()
        {
            StreamReader file = new StreamReader(Path);

            string json = file.ReadToEnd();
            List<Models.Database.Comment> items = JsonConvert.DeserializeObject<List<Models.Database.Comment>>(json);

            file.Close();

            return items;
        }

        public static void Remove( int Id )
        {
            List<Models.Database.Comment> bufferList = GetList();
            foreach (var comment in bufferList)
                if (comment.Id == Id)
                {
                    bufferList.Remove( comment );
                    break;
                }
            StreamWriter file2 = new StreamWriter(Path);
            file2.Write( JsonConvert.SerializeObject( bufferList ) );
            file2.Close();
        }
    }
}
