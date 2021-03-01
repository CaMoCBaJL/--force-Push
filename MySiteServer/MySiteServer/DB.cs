using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MySiteServer
{
    public class DB : DbContext
    {
        public DB(DbContextOptions<DB> options) : base(options) { }
        public DbSet<Good> Goods { get; set; }

    }
}
