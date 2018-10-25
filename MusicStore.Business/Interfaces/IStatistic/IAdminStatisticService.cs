using System;

namespace MusicStore.Business.Interfaces
{
    public interface IAdminStatisticService
    {
        decimal GetStatisticByTotalMoneyEarnedForSomeTime(DateTime startDate, DateTime endDate);
        int GetStatisticByNumberOfSoldSongs(DateTime startDate, DateTime endDate);
    }
}
