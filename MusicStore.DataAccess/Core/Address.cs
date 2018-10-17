using System.Collections.Generic;

namespace MusicStore.DataAccess
{
    public class Address: Entity
    {
        public Address()
        {
            User = new HashSet<User>();
        }
        public string Continent { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string House { get; set; }
        public int Flat { get; set; }
        public virtual ICollection<User> User { get; set; }
    }
}

