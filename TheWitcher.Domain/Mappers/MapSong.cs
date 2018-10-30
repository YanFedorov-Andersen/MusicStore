using MusicStore.DataAccess;


namespace MusicStore.Domain.Mappers
{
    public class MapSong : IMapper<DataAccess.Song, DataTransfer.Song>
    {
        public DataTransfer.Song AutoMap(Song item)
        {
            DataTransfer.Song song = new DataTransfer.Song()
                {
                    Id = item.Id,
                    Name = item.Name,
                    Price = item.Price,
                    Album = item.Album,
                    Artist = item.Artist,
                    Genre = item.Genre,
                };
                return song;
        }

        public Song ReverseAutoMap(DataTransfer.Song item, Song initialItem)
        {
            throw new System.NotImplementedException();
        }
    }
}
