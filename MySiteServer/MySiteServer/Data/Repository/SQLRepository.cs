using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MySiteServer.Data.Repository
{
    public class SQLRepository : IRepository
    {
        private readonly DB context;
        public SQLRepository(DB context)
        {
            this.context = context;
        }

        public void AddGood(Good newGood)
        {
            context.Goods.Add(newGood);
            context.SaveChanges();
        }

        public void DeleteGood(int goodId)
        {
            var deletedGood = context.Goods.Find(goodId);

            if (deletedGood != null)
            {
                context.Goods.Remove(deletedGood);
                context.SaveChanges();
            }
        }
        public IEnumerable<User> GetAllUsers()
        {
            return context.Users;
        }
        public IEnumerable<Producer> GetAllProducers()
        {
            return context.Producers;
        }
        public IEnumerable<Good> GetAllGoods()
        {
            return context.Goods;
        }

        public void UserInfoChanged(User changedUser)
        {
            var item = context.Users.Attach(changedUser);
            item.State = Microsoft.EntityFrameworkCore.EntityState.Modified;

            context.SaveChanges();
        }

        public void GoodChanged(Good changedGood)
        {
            var item = context.Goods.Attach(changedGood);
            item.State = Microsoft.EntityFrameworkCore.EntityState.Modified;

            context.SaveChanges();
        }

        public void AddUser(User user)
        {
            context.Add(user);
            context.SaveChanges();

        }

        public void AddProducer(Producer newProducer)
        {
            context.Add(newProducer);
            context.SaveChanges();

        }

        public void ProducerInfoChanged(Producer producer)
        {
            var item = context.Producers.Attach(producer);
            item.State = Microsoft.EntityFrameworkCore.EntityState.Modified;

            context.SaveChanges();
        }

        public void DeleteProducer(int producerId)
        {
            var deletedProducer = context.Producers.Find(producerId);

            if (deletedProducer != null)
            {
                context.Producers.Remove(deletedProducer);
                context.SaveChanges();
            }
        }

        public void DeleteUser(int userId)
        {
            var deletedUser = context.Users.Find(userId);

            if (deletedUser != null)
            {
                context.Users.Remove(deletedUser);
                context.SaveChanges();
            }
        }

        public void ChangeNewsItem(NewsItem news)
        {
            var item = context.News.Attach(news);
            item.State = Microsoft.EntityFrameworkCore.EntityState.Modified;

            context.SaveChanges();
        }

        public void AddNewsItem(NewsItem news)
        {
            context.Add(news);
            context.SaveChanges();
        }

        public IEnumerable<NewsItem> GetAllNews()
        {
            return context.News;
        }
    }
}
