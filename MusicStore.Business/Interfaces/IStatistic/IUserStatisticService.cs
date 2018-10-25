namespace MusicStore.Business.Interfaces
{
    public interface IUserStatisticService
    {
        int GetTotalNumberOfSongs(int userId);
        decimal GetTotalSpentMoney(int userId);
    }
}
