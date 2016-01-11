using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Web;
using NPORT.Models;
using Newtonsoft.Json;

namespace NPORT.Database.JSONDatabase
{
    public static class NewsJson
    {
        private static string Path = HttpContext.Current.Server.MapPath( "/App_Data/NewsDatabase.json" );

        public static void AddNews( Models.Database.News newNews )
        {
            var json = JsonSerializer.Create();

            List<Models.Database.News> newsList;

            StreamReader text2 = new StreamReader(Path);
            if (text2.EndOfStream)
                newsList = new List<Models.Database.News>();
            else
            {
                Type typ = Type.GetType("List<Models.Database.News>");
                //Newtonsoft.Json.Linq.JArray.;
                newsList = (List<Models.Database.News>)json.Deserialize( text2, typ );
            }

            newsList.Add( newNews );

            text2.Close();

            StreamWriter text = new StreamWriter(Path);
            json.Serialize( text, newsList );
            text.Close();
        }
    }
}
