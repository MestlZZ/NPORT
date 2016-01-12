﻿using System;
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

        public static void Add( Models.Database.News newNews )
        {
            StreamReader file = new StreamReader(Path);

            string json = file.ReadToEnd();
            List<Models.Database.News> items = JsonConvert.DeserializeObject<List<Models.Database.News>>(json);

            if (items == null)
                items = new List<Models.Database.News>();

            file.Close();

            items.Add( newNews );

            StreamWriter file2 = new StreamWriter(Path);

            file2.Write(JsonConvert.SerializeObject(items));

            file2.Close();
        }

        public static List<Models.Database.News> GetList()
        {
            StreamReader file = new StreamReader(Path);

            string json = file.ReadToEnd();
            List<Models.Database.News> items = JsonConvert.DeserializeObject<List<Models.Database.News>>(json);
            
            file.Close();

            return items;
        }
    }
}
