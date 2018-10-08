using System.Collections.Generic;

namespace MusicStore.DataAccess.Interfaces
{
    public interface ISongStoreRepository
    {
        List<Song> GetAvailableSongsForBuyByUser(int userId);
    }
}
