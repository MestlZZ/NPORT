using System;
using System.Collections.Generic;
using System.Web;
using Newtonsoft.Json;
using System.IO;
using NPORT.Database.Interfaces;
using NPORT.Models.Database;

namespace NPORT.Database.JSONDatabase
{
    public class CommentsDb : ICommentDatabase<Comment, int, string>
    {
        private string Path = HttpContext.Current.Server.MapPath("/App_Data/CommentsDatabase.json");

        public void Add(Comment comment)
        {
            comment.Date = DateTime.UtcNow.ToString();
            comment.Id = 1;

            List<Comment> items;

            if (File.Exists(Path))
            {
                using (StreamReader dbFile = new StreamReader(Path))
                {
                    string json = dbFile.ReadToEnd();
                    items = JsonConvert.DeserializeObject<List<Comment>>(json);
                }

                if (items.Count > 0)
                    comment.Id = items[0].Id + 1;
                else
                    items = new List<Comment>();
            }
            else
            {
                items = new List<Comment>();
            }  

            items.Insert(0, comment);

            using (StreamWriter dbFile = new StreamWriter(Path))
            {
                dbFile.Write(JsonConvert.SerializeObject(items));
            }
        }

        public Comment Find(int id)
        {
            var list = GetList();

            foreach (var comment in list)
                if (comment.Id == id)
                    return comment;

            return null;
        }

        public List<Comment> GetListForNews(string id)
        {
            var list = GetList();

            var listForNews = new List<Comment>();

            foreach (var comment in list)
                if (comment.NewsId == id)
                    listForNews.Add(comment);

            return listForNews;
        }

        public List<Comment> GetList()
        {
            string json;

            using (StreamReader file = new StreamReader(Path))
            {
                json = file.ReadToEnd();                
            }

            List<Comment> items = JsonConvert.DeserializeObject<List<Comment>>(json);

            return items;
        }

        public void Remove(int Id)
        {
            List<Comment> bufferList = GetList();

            foreach (var comment in bufferList)
                if (comment.Id == Id)
                {
                    bufferList.Remove(comment);
                    break;
                }

            using (StreamWriter file2 = new StreamWriter(Path))
            {
                file2.Write(JsonConvert.SerializeObject(bufferList));
            }

        }
    }
}
