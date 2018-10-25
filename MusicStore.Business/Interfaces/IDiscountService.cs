namespace MusicStore.Business.Interfaces
{
    public interface IDiscountService
    {
        bool IsDiscountAvailable(int userId, int albumId);
    }
}
