using MusicStore.Business.Interfaces;
using MusicStore.DataAccess;
using MusicStore.DataAccess.Interfaces;
using System;
using System.Linq;

namespace MusicStore.Business.Services.Statistics
{
    public class AdminStatisticService: IAdminStatisticService
    {
        private readonly IRepository<BoughtSong> _boughtSongRepository;

        public AdminStatisticService(IUnitOfWork unitOfWork)
        {
            _boughtSongRepository = unitOfWork.BoughtSongRepository;
        }

        public decimal GetStatisticByTotalMoneyEarnedForSomeTime(DateTime startDate, DateTime endDate)
        {
            decimal totalMoneyEarned = 0;

            if(startDate == null || endDate == null)
            {
                throw new ArgumentNullException(nameof(startDate) + ' ' + nameof(endDate), $"{nameof(startDate)} or {nameof(endDate)} is null");
            }

            var pricesList = _boughtSongRepository.GetItemList().Where(x => startDate <= x.BoughtDate && endDate > x.BoughtDate).Select(x => x.BoughtPrice);

            foreach(var price in pricesList)
            {
                totalMoneyEarned += price;
            }

            return totalMoneyEarned;
        }

        public int GetStatisticByNumberOfSoldSongs(DateTime startDate, DateTime endDate)
        {

            if (startDate == null || endDate == null)
            {
                throw new ArgumentNullException(nameof(startDate) + ' ' + nameof(endDate), $"{nameof(startDate)} or {nameof(endDate)} is null");
            }

            var numberOfSongs = _boughtSongRepository.GetItemList().Where(x => startDate <= x.BoughtDate && endDate > x.BoughtDate).Select(x => x.Id);
            return numberOfSongs.Count();
        }
    }
}
