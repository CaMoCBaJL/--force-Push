using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MySiteServer.Data.Repository
{
    public interface IRepository
    {
        IEnumerable<Good> GetAllGoods();
        void AddPerfume(string goodName, int producerId, int stackAmount, string pathToImg, int price);
        void GoodChanged(Good changedGood);
        void DeleteGood(int goodId);
    }
}
