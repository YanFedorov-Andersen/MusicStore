namespace MusicStore.Web.Models
{
    public class AdminStatisticViewModel
    {
        public decimal TotalMoneyEarnedForDay { get; set; }
        public decimal TotalMoneyEarnedForMonth { get; set; }
        public int NumberOfSoldSongsForMonth { get; set; }
        public int NumberOfSoldSongsForDay { get; set; }
        public AdminStatisticViewModel(decimal totalMoneyEarnedForDay, decimal totalMoneyEarnedForMonth, int numberOfSoldSongsForMonth,
            int numberOfSoldSongsForDay)
        {
            TotalMoneyEarnedForDay = totalMoneyEarnedForDay;
            TotalMoneyEarnedForMonth = totalMoneyEarnedForMonth;
            NumberOfSoldSongsForDay = numberOfSoldSongsForDay;
            NumberOfSoldSongsForMonth = numberOfSoldSongsForMonth;
        }
    }
}
