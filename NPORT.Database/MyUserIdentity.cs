using System.Security.Principal;

namespace NPORT
{
    public class MyUserIdentity : IIdentity
    {
        public MyUserIdentity( string name, string authenticationType, bool isAuthenticated, string userId, int role )
        {
            Name = name;
            AuthenticationType = authenticationType;
            IsAuthenticated = isAuthenticated;
            UserId = userId;
            Role = role;
        }

        #region IIdentity
        public string Name { get; private set; }
        public string AuthenticationType { get; private set; }
        public bool IsAuthenticated { get; private set; }
        #endregion

        public string UserId { get; private set; }

        public int Role { get; private set; }
    }
}
