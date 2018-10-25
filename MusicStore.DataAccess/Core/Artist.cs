using System.Collections.Generic;

namespace MusicStore.DataAccess
{
    public class Artist : Entity
    {
        public Artist()
        {
            Songs = new HashSet<Song>();
        }
        public Artist(string firstName, string lastName)
        {
            LastName = lastName;
            FirstName = firstName;
        }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public virtual ICollection<Song> Songs { get; set; }
    }
}
