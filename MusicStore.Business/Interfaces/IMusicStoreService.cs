using System.Collections.Generic;
using MusicStore.DataAccess.Realization;
using MusicStore.Domain.DataTransfer;

namespace MusicStore.Business.Interfaces
{
    public interface IMusicStoreService
    {
        BoughtSong BuySong(int songId, int userId);
    }
}
