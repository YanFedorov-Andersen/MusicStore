using MusicStore.Business.Interfaces;
using MusicStore.DataAccess;
using MusicStore.DataAccess.Interfaces;
using System;
using System.Linq;

namespace MusicStore.Business.Services.Statistics
{
    public class AdminStatisticService : IAdminStatisticService
    {
        private readonly IRepository<BoughtSong> _boughtSongRepository;

        public AdminStatisticService(IUnitOfWork unitOfWork)
        {
            _boughtSongRepository = unitOfWork.BoughtSongRepository;
        }

        public decimal GetStatisticByTotalMoneyEarnedForSomeTime(DateTime startDate, DateTime endDate)
        {
            decimal totalMoneyEarned = 0;
            var emptyDateTime = new DateTime();
            if (startDate == emptyDateTime || endDate == emptyDateTime)
            {
                throw new ArgumentException($"{nameof(startDate)} or {nameof(endDate)} is null",
                    $"{nameof(startDate)}' '{nameof(endDate)}");
            }

            var pricesList = _boughtSongRepository.GetItemList()
                .Where(x => startDate <= x.BoughtDate && endDate > x.BoughtDate)
                .Select(x => x.BoughtPrice);

            totalMoneyEarned = pricesList.Sum();

            return totalMoneyEarned;
        }

        public int GetStatisticByNumberOfSoldSongs(DateTime startDate, DateTime endDate)
        {

            var emptyDateTime = new DateTime();
            if (startDate == emptyDateTime || endDate == emptyDateTime)
            {
                throw new ArgumentException($"{nameof(startDate)} or {nameof(endDate)} is null",
                    $"{nameof(startDate)}' '{nameof(endDate)}");
            }

            var numberOfSongs = _boughtSongRepository.GetItemList()
                .Where(x => startDate <= x.BoughtDate && endDate > x.BoughtDate)
                .Select(x => x.Id);

            return numberOfSongs.Count();
        }
    }
}
