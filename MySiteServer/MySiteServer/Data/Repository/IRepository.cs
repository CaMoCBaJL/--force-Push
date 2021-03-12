using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MySiteServer.Data.Repository
{
    public interface IRepository
    {
        void ChangeNewsItem(NewsItem news);
        void AddNewsItem(NewsItem news);
        IEnumerable<NewsItem> GetAllNews();
        void AddProducer(Producer producer);
        void ProducerInfoChanged(Producer producer);
        void DeleteProducer(int producerId);
        IEnumerable<Producer> GetAllProducers();
        void AddUser(User user);
        void UserInfoChanged(User changedUser);
        void DeleteUser(int userId);
        IEnumerable<User> GetAllUsers();
        void AddGood(Good newGood);
        void GoodChanged(Good changedGood);
        void DeleteGood(int goodId);
        IEnumerable<Good> GetAllGoods();
    }
}
