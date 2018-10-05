using System.Collections.Generic;
using TheWitcher.Domain.Models;

namespace MusicStore.Business.Interfaces
{
    public interface IMusicStoreService
    {
        List<SongDTO> DisplayAllAvailableSongs(int userId);
        bool BuySong(int songId, int userId);
    }
}
