using MusicStore.DataAccess;
using TheWitcher.Domain.Models;

namespace TheWitcher.Domain.Mappers
{
    public class MapSong : IMapper<Song, SongDTO>
    {
        public SongDTO AutoMap(Song item)
        {
                SongDTO song = new SongDTO()
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
    }
}
