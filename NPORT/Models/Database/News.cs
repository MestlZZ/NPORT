
namespace NPORT.Models.Database
{
    public class News
    {
        public string Id { get; set; }

        public string Title { get; set; }

        public string ShortInfo { get; set; }

        public int VisibleRange { get; set; }

        public string Content { get; set; }

        public string AuthorId { get; set; }

        public string Date { get; set; }
    }
}
