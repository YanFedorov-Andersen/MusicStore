using System.Collections.Generic;

namespace MusicStore.DataAccess
{
    public class Address: Entity
    {
        public Address()
        {
            User = new HashSet<User>();
        }
        public Address(string continent, string country, string city, string street, string house, int flat)
        {
            City = city;
            Continent = continent;
            Country = country;
            Street = street;
            House = house;
            Flat = flat;
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

