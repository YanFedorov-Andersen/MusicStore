namespace MusicStore.Domain.Mappers
{
    public class MapBoughtSong : IMapper<DataAccess.BoughtSong, DataTransfer.BoughtSong>
    {
        public DataTransfer.BoughtSong AutoMap(DataAccess.BoughtSong item)
        {
            DataTransfer.BoughtSong boughtSong = new DataTransfer.BoughtSong()
            {
                Id = item.Id,
                BoughtDate = item.BoughtDate,
                BoughtPrice = item.BoughtPrice,
                Song = item.Song
            };
            return boughtSong;
        }
    }
}
