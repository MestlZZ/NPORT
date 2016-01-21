using System.Text;
using System.Security.Cryptography;
using Microsoft.AspNet.Identity;

namespace NPORT.Identity
{
    public class CustomPasswordHasher : PasswordHasher
    {
        public override string HashPassword( string password )
        {
            var md = MD5.Create();
            Encoding u8 = Encoding.UTF8;

            byte[] buff = u8.GetBytes(password);
            buff = md.ComputeHash( buff );

            char[] chars = new char[buff.Length / sizeof(char)];
            System.Buffer.BlockCopy( buff, 0, chars, 0, buff.Length );

            password = new string( chars );
            return password;
        }

        public override PasswordVerificationResult VerifyHashedPassword( string hashedPassword, string providedPassword )
        {
            if (hashedPassword == HashPassword( providedPassword ))
            {
                return PasswordVerificationResult.SuccessRehashNeeded;
            }
            else
            {
                return PasswordVerificationResult.Failed;
            }
        }
    }
}