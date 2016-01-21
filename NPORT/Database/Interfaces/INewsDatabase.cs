using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NPORT.Database.Interfaces
{
    interface INewsDatabase<TNews, TKey>
    {
        void Add(TNews news);

        List<TNews> GetList();

        TNews Find(TKey id);

        void Edit(TNews news);

        void Remove(TKey id);
    }
}
