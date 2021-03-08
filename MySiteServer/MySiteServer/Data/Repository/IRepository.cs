using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MySiteServer.Data.Repository
{
    public interface IRepository
    {
        void AddUser(User user);
        void RememberUser(int id);
        IEnumerable<User> GetAllUsers();
        IEnumerable<Producer> GetAllProducers();
        IEnumerable<Good> GetAllGoods();
        void AddPerfume(int goodId, string goodName, int producerId, int stackAmount, int price);
        void GoodChanged(Good changedGood);
        void DeleteGood(int goodId);
    }
}
