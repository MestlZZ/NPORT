namespace NPORT.Models.Database
{
    public class Role
    {
        public Role()
        { }

        public int Id { get; set; }

        public string Name { get; set; }

        public bool Access_RemoveNews { get; set; }

        public bool Access_AddNews { get; set; }

        public bool Access_EditNews { get; set; }
    }
}
