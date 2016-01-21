using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using Newtonsoft.Json;

namespace NPORT.Database.JSONDatabase
{
    public static class NewsDb
    {
        private static string Path = HttpContext.Current.Server.MapPath( "/App_Data/NewsDatabase.json" );

        public static void Add( Models.Database.News newNews )
        {
            newNews.Date = DateTime.UtcNow.ToString();
            newNews.Id = Guid.NewGuid().ToString();

            string json;

            using (StreamReader dbFile = new StreamReader(Path))
            {
                json = dbFile.ReadToEnd();
            }

            List<Models.Database.News> items = JsonConvert.DeserializeObject<List<Models.Database.News>>(json);

            if (items == null)
            {
                items = new List<Models.Database.News>();
            }

            items.Insert(0 , newNews);

            using (StreamWriter dbFile = new StreamWriter(Path))
            {
                dbFile.Write(JsonConvert.SerializeObject(items));
            }

        }

        public static Models.Database.News Find(string id)
        {
            var list = GetList();

            foreach (var news in list)
                if (news.Id == id)
                    return news;

            return null;
        }

        public static List<Models.Database.News> GetList()
        {
            string json;

            using (StreamReader dbFile = new StreamReader(Path))
            {
                json = dbFile.ReadToEnd();
            }

            return JsonConvert.DeserializeObject<List<Models.Database.News>>(json);
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

            using (StreamWriter dbFile = new StreamWriter(Path))
            {
                dbFile.Write(JsonConvert.SerializeObject(bufferList));
            }
        }

        public static void Remove(string NewsId)
        {
            var comments = CommentsDb.GetList();

            foreach (var comment in comments)
            {
                if (comment.NewsId == NewsId)
                    CommentsDb.Remove(comment.Id);
            }

            List<Models.Database.News> bufferList = GetList();

            foreach (var news in bufferList)
            {
                if (news.Id == NewsId)
                {
                    bufferList.Remove(news);
                    break;
                }
            }

            using (StreamWriter dbFile = new StreamWriter(Path))
            {
                dbFile.Write(JsonConvert.SerializeObject(bufferList));
            }
        }
    }
}
