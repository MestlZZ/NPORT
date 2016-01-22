using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using Newtonsoft.Json;
using NPORT.Models.Database;
using NPORT.Database.Interfaces;

namespace NPORT.Database.JSONDatabase
{
    public class NewsDb : INewsDatabase<News, string>
    {
        private string Path = HttpContext.Current.Server.MapPath( "/App_Data/NewsDatabase.json" );

        public void Add( News newNews )
        {
            newNews.Date = DateTime.UtcNow.ToString();
            newNews.Id = Guid.NewGuid().ToString();

            List<News> items = GetAllNews() ?? new List<News>();            

            items.Insert(0, newNews);

            using (StreamWriter dbFile = new StreamWriter(Path))
            {
                dbFile.Write(JsonConvert.SerializeObject(items));
            }

        }

        private List<News> GetAllNews()
        {
            string json = ReadJson();

            List<News> items = JsonConvert.DeserializeObject<List<News>>(json);
            return items;
        }

        private string ReadJson()
        {
            string json;

            using (StreamReader dbFile = new StreamReader(Path))
            {
                json = dbFile.ReadToEnd();
            }

            return json;
        }

        public News Find(string id)
        {
            var list = GetList();

            foreach (var news in list)
                if (news.Id == id)
                    return news;

            return null;
        }

        public List<News> GetList()
        {
            string json;

            using (StreamReader dbFile = new StreamReader(Path))
            {
                json = dbFile.ReadToEnd();
            }

            return JsonConvert.DeserializeObject<List<Models.Database.News>>(json);
        }

        public void Edit(News news)
        {
            List<News> bufferList = GetList();

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

        public void Remove(string NewsId)
        {
            var commentsDb = new CommentsDb();

            var comments = commentsDb.GetList();

            foreach (var comment in comments)
            {
                if (comment.NewsId == NewsId)
                    commentsDb.Remove(comment.Id);
            }

            List<News> bufferList = GetList();

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
