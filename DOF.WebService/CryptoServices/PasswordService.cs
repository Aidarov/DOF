using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace DOF.WebService.CryptoServices
{
    public class PasswordService
    {
        public static byte[] GenerateSalt()
        {            
            using (var rngCsp = new RNGCryptoServiceProvider())
            {
                byte[] salt = new byte[128 / 8];

                rngCsp.GetNonZeroBytes(salt);

                return salt;
            }
        }

        public static string GetSaltInBase64()
        {
            var salt = GenerateSalt();
            return Convert.ToBase64String(salt);
        }

        public static string GetSaltInBase64(byte[] salt)
        {
            return Convert.ToBase64String(salt);
        }

        public static string GeneratePassword(string password, byte[] salt)
        {
            return Convert.ToBase64String(KeyDerivation.Pbkdf2(
            password: password,
            salt: salt,
            prf: KeyDerivationPrf.HMACSHA256,
            iterationCount: 100000,
            numBytesRequested: 256 / 8));
        }
    }
}
