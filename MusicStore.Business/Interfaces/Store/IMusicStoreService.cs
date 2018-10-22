using MusicStore.Domain.DataTransfer;

namespace MusicStore.Business.Interfaces
{
    public interface IMusicStoreService
    {
        BoughtSong BuySong(int songId, int userId, decimal discount = 0);
    }
}
