using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NPORT.Database.Interfaces
{
    interface IRoleDatabase<TRole, TKey>
    {
        List<TRole> GetList();

        TRole Find(TKey id);
    }
}
