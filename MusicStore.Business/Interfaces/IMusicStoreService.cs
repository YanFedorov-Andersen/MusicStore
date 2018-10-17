using System.Collections.Generic;
using MusicStore.Domain.DataTransfer;

namespace MusicStore.Business.Interfaces
{
    public interface IMusicStoreService
    {
        IList<Song> DisplayAllAvailableSongs(int userId);
        BoughtSong BuySong(int songId, int userId);
        IList<Domain.DataTransfer.Song> DisplayAllSongs();
    }
}
