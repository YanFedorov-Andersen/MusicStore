namespace MusicStore.DataAccess.Core
{
    abstract class SongAbstractFactory
    {
        public abstract Genre SetGenre();
        public abstract Artist SetArtist();
    }
    class PopSongFactory: SongAbstractFactory
    {
        public override Genre SetGenre()
        {
            return new Pop();
        }
        public override Artist SetArtist()
        {
            return new Baskov();
        }
    }
    class RockSongFactory : SongAbstractFactory
    {
        public override Genre SetGenre()
        {
            return new Rock();
        }
        public override Artist SetArtist()
        {
            return new Kirkorov();
        }
    }

    class SongCreatedByFactory
    {
        private Artist artist { get; set; }
        private Genre genre { get; set; }

        public SongCreatedByFactory(SongAbstractFactory factory)
        {
            artist = factory.SetArtist();
            genre = factory.SetGenre();
        }
        
        public string GetGenre()
        {
            return genre.Name;
        }
        public string GetArtist()
        {
            return artist.LastName;
        }
    }

    class Pop: Genre
    {
        public Genre SetGenre()
        {
            return new Genre()
            {
                Name = "Pop"
            };
        }
    }
    class Rock: Genre
    {
        public Genre SetGenre()
        {
            return new Genre()
            {
                Name = "Rock"
            };
        }
    }
    class Kirkorov: Artist
    {
        public Artist SetArtist()
        {
            return new Artist()
            {
                LastName = "Kirkorov"
            };
        }
    }
    class Baskov: Artist
    {
        public Artist SetArtist()
        {
            return new Artist()
            {
                LastName = "Baskov"
            };
        }
    }

}
