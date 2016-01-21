using NPORT.Database.XMLDatabase;

namespace NPORT.Models.Database
{
    public class Comment
    {
        public int Id { get; set; }

        public string AuthorId { get; set; }

        public string Text { get; set; }

        public string Date { get; set; }

        public string NewsId { get; set; }

        public string GetAuthorName()
        {
            return UsersDb.Find(AuthorId).UserName;
        }
    }
}
