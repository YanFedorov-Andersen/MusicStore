namespace MusicStore.Web.Models
{
    public class UserStatisticViewModel
    {
        public int TotalNumberOfSongs { get; set; }
        public decimal TotalSpentMoney { get; set; }

        public UserStatisticViewModel(int totalNumberOfSongs, decimal totalSpentMoney)
        {
            TotalNumberOfSongs = totalNumberOfSongs;
            TotalSpentMoney = totalSpentMoney;
        }
    }
}