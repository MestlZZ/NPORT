using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using Newtonsoft.Json;

namespace NPORT.Database.JSONDatabase
{
    public static class NewsJson
    {
        private static string Path = HttpContext.Current.Server.MapPath( "/App_Data/NewsDatabase.json" );

        public static void Add( Models.Database.News newNews )
        {
            newNews.Date = DateTime.UtcNow.ToString();
            newNews.Id = Guid.NewGuid().ToString();

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

        public static Models.Database.News Find( string id )
        {
            var list = GetList();
            foreach (var news in list)
                if (news.Id == id)
                    return news;
            return null;
        }

        public static List<Models.Database.News> GetList()
        {
            StreamReader file = new StreamReader(Path);

            string json = file.ReadToEnd();
            List<Models.Database.News> items = JsonConvert.DeserializeObject<List<Models.Database.News>>(json);
            
            file.Close();

            return items;
        }
        public static void Edit(Models.Database.News news)
        {
            List<Models.Database.News> bufferList = GetList();
            for (int i=0; i<bufferList.Count; i++)
            {
                if(bufferList[i].Id==news.Id)
                {
                    bufferList[i] = news;
                }
            }
            StreamWriter file2 = new StreamWriter(Path);

            file2.Write(JsonConvert.SerializeObject(bufferList));

            file2.Close();
        }
        public static void Remove(string NewsId)
        {
            List<Models.Database.News> bufferList = GetList();
            foreach (var news in bufferList)
                if (news.Id == NewsId)
                {
                    bufferList.Remove(news);
                    break;
                }            
            StreamWriter file2 = new StreamWriter(Path);
            file2.Write(JsonConvert.SerializeObject(bufferList));
            file2.Close();
        }
    }
}
