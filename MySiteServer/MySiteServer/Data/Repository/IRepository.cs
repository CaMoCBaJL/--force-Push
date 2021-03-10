using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MySiteServer.Data.Repository
{
    public interface IRepository
    {
        void AddProducer(Producer producer);
        void ProducerInfoChanged(Producer producer);
        void DeleteProducer(int producerId);
        void AddUser(User user);
        void UserInfoChanged(User changedUser);
        void DeleteUser(int userId);
        IEnumerable<User> GetAllUsers();
        IEnumerable<Producer> GetAllProducers();
        IEnumerable<Good> GetAllGoods();
        void AddGood(Good newGood);
        void GoodChanged(Good changedGood);
        void DeleteGood(int goodId);
    }
}
