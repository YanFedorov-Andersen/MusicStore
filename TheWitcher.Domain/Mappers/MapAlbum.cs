using MusicStore.DataAccess;
using System;

namespace MusicStore.Domain.Mappers
{
    public class MapAlbum : IMapper<Album, DataTransfer.Album>
    {
        public DataTransfer.Album AutoMap(Album item)
        {
            DataTransfer.Album domainAlbum = new DataTransfer.Album()
            {
                Id = item.Id,
                DiscountIfBuyAllSongs = item.DiscountIfBuyAllSongs,
                Name = item.Name,
                Songs = item.Songs
            };
            return domainAlbum;
        }

        public DataAccess.Album ReAutoMap(DataTransfer.Album item, DataAccess.Album initialItem)
        {
            throw new NotImplementedException();
        }
    }
}
