using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Linq;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace API.Helpers
{
    public static class HashHelper
    {
        public static hashedPassword Hash(string password)
        {
            byte[] salt= new byte [128/8];
            using(var rng=RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }
            // deriva a 256-bit (use hmacsha1)
            string hashed=Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password : password,
                salt:salt,
                prf : KeyDerivationPrf.HMACSHA256,
                iterationCount:10000,
                numBytesRequested : 256/8
            ));

            return new hashedPassword(){
                Password=hashed, Salt = Convert.ToBase64String(salt)
            };
        }

        public static bool CheckHash(string attemptedPassword, string hash, string salt)
        {
            string hashed=Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password : attemptedPassword,
                salt : Convert.FromBase64String(salt),
                prf : KeyDerivationPrf.HMACSHA256,
                iterationCount : 10000,
                numBytesRequested : 256/8   
            ));

            return hash==hashed;
        }

        



    }




    public class hashedPassword
    {
        public string Password { get; set; }
        public string Salt { get; set; }

    }
}