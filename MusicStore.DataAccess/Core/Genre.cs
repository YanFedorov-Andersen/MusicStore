namespace MusicStore.DataAccess
{
    public class Genre : Entity
    {
        public Genre()
        {

        }
        public Genre(string name)
        {
            Name = name;
        }
        public string Name { get; set; }
    }
}
