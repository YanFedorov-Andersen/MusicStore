using System.Collections.Generic;

namespace MusicStore.Domain.DataTransfer
{
    public class Album
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal DiscountIfBuyAllSongs { get; set; }
        public virtual ICollection<DataAccess.Song> Songs { get; set; }
        //TODO: с моделью дто передаётся коллекция data accsess,  а если мапить и коллекции, может получится циклическая зависимость
    }
}
