using System.Collections.Generic;
using MusicStore.DataAccess.Realization;
using MusicStore.Domain.DataTransfer;

namespace MusicStore.Business.Interfaces
{
    public interface IMusicStoreDisplayService
    {
        IList<Song> DisplayAllAvailableSongs(int userId);
        IList<Domain.DataTransfer.Song> DisplayAllSongs();
        IndexViewItem<Domain.DataTransfer.Album> DisplayAlbumsWithPagination(int page);
    }
}
