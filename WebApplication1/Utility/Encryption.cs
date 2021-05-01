using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication1.Utility
{
    public class SymmetricKeys
    {
        public byte[] SecretKey { get; set; }
        public byte[] Iv { get; set; }
    }

    public class AsymmetricKeys
    {
        public string PublicKey { get; set; }
        public string PrivateKey { get; set; }
    }
    public class Encryption
    {
        //generating a secret key from this password & adding more security when using salt
        static string password = "Pas$$w0rd";
        static byte[] salt = new byte[]
        {
            20,1,203,45,46,78,89,76,54,31,222,111,42,59,67,234,213,56
        };

        public static byte[] Hash(byte[] clearTextBytes)
        {
            SHA512 myAlg = SHA512.Create();
            byte[] digest = myAlg.ComputeHash(clearTextBytes);

            return digest;
        }

        public static SymmetricKeys GenerateKeys()
        {
            Rijndael myAlg = Rijndael.Create();

            //Generating the Keys
            Rfc2898DeriveBytes myGenerator = new Rfc2898DeriveBytes(password, salt);

            SymmetricKeys keys = new SymmetricKeys()
            {
                SecretKey = myGenerator.GetBytes(myAlg.KeySize / 8),
                Iv = myGenerator.GetBytes(myAlg.BlockSize / 8)
            };

            return keys;
        }

        //Taking in the original data and return the encrypted data
        public static byte[] SymmetricEncrypt(byte[] clearData)
        {
            Rijndael myAlg = Rijndael.Create();
            var keys = GenerateKeys();

            //Loading the data we are about to encrypt
            MemoryStream msIn = new MemoryStream(clearData);
            msIn.Position = 0;

            MemoryStream msOut = new MemoryStream();
            CryptoStream cs = new CryptoStream(msOut, myAlg.CreateEncryptor(keys.SecretKey, keys.Iv), CryptoStreamMode.Write);
            msIn.CopyTo(cs);
            cs.FlushFinalBlock();
            cs.Close();

            return msOut.ToArray();
        }

        public static byte[] SymmetricDecrypt(byte[] cipherAsBytes)
        {
            Rijndael myAlg = Rijndael.Create();
            var keys = GenerateKeys();

            MemoryStream msIn = new MemoryStream(cipherAsBytes);
            msIn.Position = 0;

            MemoryStream msOut = new MemoryStream();
            CryptoStream cs = new CryptoStream(msOut, myAlg.CreateDecryptor(keys.SecretKey, keys.Iv), CryptoStreamMode.Write);
            msIn.CopyTo(cs);
            cs.FlushFinalBlock();
            cs.Close();

            return msOut.ToArray();
        }

        public static string SymmetricEncrypt(string clearData)
        {
            //Converting from string to an array of bytes[] - encrypting
            byte[] clearDataAsBytes = Encoding.UTF32.GetBytes(clearData);
            byte[] cipherAsBytes = SymmetricEncrypt(clearDataAsBytes);

            //Converting back to string
            string cipher = Convert.ToBase64String(cipherAsBytes);

            string x = "";
            if (cipher.Contains("/") || cipher.Contains("+") || cipher.Contains("="))
            {
                string t = cipher.Replace("/", "|");
                string a = t.Replace("+", "_");
                x = a.Replace("=", "$");

                return x;
            }
            return cipher;
        }

        public static string SymmetricDecrypt(string cipher)
        {
            byte[] cipherDataAsBytes;
            if (cipher.Contains("|") || cipher.Contains("_") || cipher.Contains("$"))
            {
                string t = cipher.Replace("|", "/");
                string a = t.Replace("_", "+");
                string x = a.Replace("$", "=");

                //Converting from string to an array of bytes[] - encrypting
                cipherDataAsBytes = Convert.FromBase64String(x);
            }
            else
            {
                cipherDataAsBytes = Convert.FromBase64String(cipher);
            }

            byte[] clearDataAsBytes = SymmetricDecrypt(cipherDataAsBytes);

            //Converting back to string
            string originalText = Encoding.UTF32.GetString(clearDataAsBytes);
            return originalText;
        }

        public AsymmetricKeys GenerateAsymmetricKeys()
        {
            RSACryptoServiceProvider myAlg = new RSACryptoServiceProvider();
            AsymmetricKeys myKeys = new AsymmetricKeys()
            {
                PublicKey = myAlg.ToXmlString(false),
                PrivateKey = myAlg.ToXmlString(true)
            };

            return myKeys;
        }

        public string AsymmetricEncrypt(string data, string publicKey)
        {
            RSACryptoServiceProvider myAlg = new RSACryptoServiceProvider();
           
            myAlg.FromXmlString(publicKey);
            byte[] dataAsBytes = Encoding.UTF32.GetBytes(data);
            byte[] cipher = myAlg.Encrypt(dataAsBytes, RSAEncryptionPadding.Pkcs1);

            return Convert.ToBase64String(cipher);
        }

        public string AsymmetricDecrypt(string cipher, string privateKey)
        {
            RSACryptoServiceProvider myAlg = new RSACryptoServiceProvider();

            byte[] cipherAsBytes = Convert.FromBase64String(cipher);
            byte[] originalText = myAlg.Decrypt(cipherAsBytes, RSAEncryptionPadding.Pkcs1);

            return Encoding.UTF32.GetString(originalText);
        }
        //With the AsymmetricEncrypt we are going to encrypt the symmetric keys

        public MemoryStream HybridEnryption(MemoryStream clearFile, string publicKey)
        {
            Rijndael myAlg = Rijndael.Create();
            myAlg.GenerateKey();
            myAlg.GenerateIV();

            var key = myAlg.Key;
            var iv = myAlg.IV;

            
        }
    }
}
