using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicStore.Business.Interfaces
{
    public interface IDiscountService
    {
        bool CheckDiscountAvailable(int userId, int albumId);
    }
}
