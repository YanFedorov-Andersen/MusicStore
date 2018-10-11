using System.Collections.Generic;
using MusicStore.Domain.DataTransfer;

namespace MusicStore.Business.Interfaces
{
    public interface IMusicStoreService
    {
        List<Song> DisplayAllAvailableSongs(int userId);
        BoughtSong BuySong(int songId, int userId);
        List<Domain.DataTransfer.Song> DisplayAllSongs();
    }
}
