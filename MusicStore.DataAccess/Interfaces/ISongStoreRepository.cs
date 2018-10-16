using System.Collections.Generic;

namespace MusicStore.DataAccess.Interfaces
{
    public interface ISongStoreRepository
    {
        IList<Song> GetAvailableSongsForBuyByUser(int userId);
    }
}
