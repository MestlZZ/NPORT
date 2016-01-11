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
            StreamWriter text = new StreamWriter(Path);
            json.Serialize( text, newNews );
            text.Close();
        }
    }
}
