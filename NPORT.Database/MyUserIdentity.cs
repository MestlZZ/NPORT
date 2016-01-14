using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Principal;

namespace NPORT
{
    public class MyUserIdentity : IIdentity
    {
        public MyUserIdentity( string name, string authenticationType, bool isAuthenticated, string userId )
        {
            Name = name;
            AuthenticationType = authenticationType;
            IsAuthenticated = isAuthenticated;
            UserId = userId;
        }

        #region IIdentity
        public string Name { get; private set; }
        public string AuthenticationType { get; private set; }
        public bool IsAuthenticated { get; private set; }
        #endregion

        public string UserId { get; private set; }
    }
}
