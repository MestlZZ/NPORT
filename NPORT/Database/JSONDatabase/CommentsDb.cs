﻿using System;
using System.Collections.Generic;
using System.Web;
using Newtonsoft.Json;
using System.IO;

namespace NPORT.Database.JSONDatabase
{
    public class CommentsDb
    {
        private static string Path = HttpContext.Current.Server.MapPath("/App_Data/CommentsDatabase.json");

        public static void Add(Models.Database.Comment comment)
        {
            comment.Date = DateTime.UtcNow.ToString();
            comment.Id = 1;

            List<Models.Database.Comment> items;

            if (File.Exists(Path))
            {
                using (StreamReader dbFile = new StreamReader(Path))
                {
                    string json = dbFile.ReadToEnd();
                    items = JsonConvert.DeserializeObject<List<Models.Database.Comment>>(json);
                }

                if (items.Count > 0)
                    comment.Id = items[0].Id + 1;
                else
                    items = new List<Models.Database.Comment>();
            }
            else
            {
                items = new List<Models.Database.Comment>();
            }  

            items.Insert(0, comment);

            using (StreamWriter dbFile = new StreamWriter(Path))
            {
                dbFile.Write(JsonConvert.SerializeObject(items));
            }
        }

        public static Models.Database.Comment Find(int id)
        {
            var list = GetList();

            foreach (var comment in list)
                if (comment.Id == id)
                    return comment;

            return null;
        }

        public static List<Models.Database.Comment> GetListForNews(string id)
        {
            var list = GetList();

            var listForNews = new List<Models.Database.Comment>();

            foreach (var comment in list)
                if (comment.NewsId == id)
                    listForNews.Add(comment);

            return listForNews;
        }

        public static List<Models.Database.Comment> GetList()
        {
            string json;

            using (StreamReader file = new StreamReader(Path))
            {
                json = file.ReadToEnd();                
            }

            List<Models.Database.Comment> items = JsonConvert.DeserializeObject<List<Models.Database.Comment>>(json);

            return items;
        }

        public static void Remove(int Id)
        {
            List<Models.Database.Comment> bufferList = GetList();

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