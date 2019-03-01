using System;
using System.Security.Cryptography;
using System.Text;


namespace Platformer
{
    public class HashData
    {
        public HashData()
        {
            string ComputeHash(string plainText, byte[] salt)
            {
                int minSaltLength = 4;
                int maxSaltLength = 16;
                byte[] SaltBytes = null;
                if (salt != null)
                {
                    SaltBytes = salt;
                }
               


                else
                {
                    Random r = new Random();
                    int SaltLength = r.Next(minSaltLength, maxSaltLength);
                    SaltBytes = new byte[SaltLength];
                    RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
                    rng.GetNonZeroBytes(SaltBytes);
                    rng.Dispose();

                    byte[] plainData = ASCIIEncoding.UTF8.GetBytes(plainText);
                    byte[] plainDataAndSalt = new byte[plainData.Length + SaltBytes.Length];

                    for (int x = 0; x < plainData.Length; x++)
                        plainDataAndSalt[x] = plainData[x];
                    for (int n = 0; n < SaltBytes.Length; n++)
                        plainDataAndSalt[plainData.Length + n] = SaltBytes[n];

                    byte[] hashVal = null;
                    SHA256Managed sha = new SHA256Managed();
                    hashVal = sha.ComputeHash(plainDataAndSalt);
                    sha.Dispose();

                    byte[] result = new byte[hashVal.Length + SaltBytes.Length];
                    for (int x = 0; x < hashVal.Length; x++)
                        result[x] = hashVal[x];
                    for (int n = 0; n < SaltBytes.Length; n++)
                        result[hashVal.Length + n] + SaltBytes[n];



                    return Convert.ToBase64String(result);


                }

                bool Confirm(string plainText, string hashVal)
                {
                    byte[] hashBytes = Convert.FromBase64String(hashVal);
                    int hashSize = 32;
                    byte[] saltBytes = new byte[hashBytes.Length - hashSize];
                    for (int x = 0; x < SaltBytes.Length; x++)
                    {
                        SaltBytes[x] = hashBytes[hashSize + x];

                    }
                    string NewHash = ComputeHash(plainText, SaltBytes);

                    return (hashVal == NewHash);
                }


            }




        }
    }
}
