using MusicStore.Web.Models;

namespace MusicStore.Web
{
    public class SeedData
    {
        public static void Initialize(ApplicationDbContext _modelContext)
        {
            _modelContext.Database.Exists();

            



            //if (!_modelContext.Addresses.Any())
            //{
            //    _modelContext.Addresses.Add(address1);
            //    _modelContext.Addresses.Add(address1);
            //}

            _modelContext.SaveChanges();
        }
    }
}