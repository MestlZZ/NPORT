using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NPORT.Database.Interfaces
{
    interface IUserDatabase<TUser, TKey>
    {
        List<TUser> GetList();

        TUser Find(TKey id);

        TUser FindByUsername(string username);

        TUser FindByLogin(string login);

        void Update(List<TUser> users);
    }
}
