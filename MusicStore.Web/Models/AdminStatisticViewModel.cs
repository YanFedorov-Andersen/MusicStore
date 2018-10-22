namespace MusicStore.Web.Models
{
    public class AdminStatisticViewModel
    {
        public decimal TotalMoneyEarnedForDay { get; set; }
        public decimal TotalMoneyEarnedForMonth { get; set; }
        public int NumberOfSoldSongsForMonth { get; set; }
        public int NumberOfSoldSongsForDay { get; set; }
    }
}
