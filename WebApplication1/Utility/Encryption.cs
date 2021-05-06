using AssignmentTask.Application.Interfaces;
using AssignmentTask.Application.Services;
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

        private readonly IAssignmentsService assignmentsService;

        public Encryption(IAssignmentsService _assignmentsService)
        {
            _assignmentsService = assignmentsService;
        }


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
        public static byte[] SymmetricEncrypt(byte[] clearData, byte[] iv, byte[] secretKey)
        {
            Rijndael myAlg = Rijndael.Create();

            //Loading the data we are about to encrypt
            MemoryStream msIn = new MemoryStream(clearData);
            msIn.Position = 0;

            MemoryStream msOut = new MemoryStream();
            CryptoStream cs = new CryptoStream(msOut, myAlg.CreateEncryptor(secretKey, iv), CryptoStreamMode.Write);
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

        public static byte[] SymmetricHybridDecrypt(byte[] iv, byte[] secretKey, byte[] cipherAsBytes)
        {
            Rijndael myAlg = Rijndael.Create();

            MemoryStream msIn = new MemoryStream(cipherAsBytes);
            msIn.Position = 0;

            MemoryStream msOut = new MemoryStream();
            CryptoStream cs = new CryptoStream(msOut, myAlg.CreateDecryptor(secretKey, iv), CryptoStreamMode.Write);
            msIn.CopyTo(cs);
            cs.FlushFinalBlock();
            cs.Close();

            return msOut.ToArray();
        }

        public static string SymmetricEncrypt(string clearData)
        {
            var keys = GenerateKeys();

            //Converting from string to an array of bytes[] - encrypting
            byte[] clearDataAsBytes = Encoding.UTF32.GetBytes(clearData);
            byte[] cipherAsBytes = SymmetricEncrypt(clearDataAsBytes, keys.Iv, keys.SecretKey);

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

        public static AsymmetricKeys GenerateAsymmetricKeys()
        {
            RSACryptoServiceProvider myAlg = new RSACryptoServiceProvider();
            AsymmetricKeys myKeys = new AsymmetricKeys()
            {
                PublicKey = myAlg.ToXmlString(false),
                PrivateKey = myAlg.ToXmlString(true)
            };

            return myKeys;

        }

        public static byte[] AsymmetricEncrypt(byte[] data, string publicKey)
        {
            RSACryptoServiceProvider myAlg = new RSACryptoServiceProvider();

            myAlg.FromXmlString(publicKey);

            byte[] cipher = myAlg.Encrypt(data, RSAEncryptionPadding.Pkcs1);

            return cipher;
        }

        public static byte[] AsymmetricDecrypt(byte[] cipher, string privateKey)
        {
            RSACryptoServiceProvider myAlg = new RSACryptoServiceProvider();
            myAlg.FromXmlString(privateKey);

            byte[] originalText = myAlg.Decrypt(cipher, RSAEncryptionPadding.Pkcs1);

            return originalText;
        }
        //With the AsymmetricEncrypt we are going to encrypt the symmetric keys

        public static MemoryStream HybridEnryption(MemoryStream clearFile, string publicKey)
        {
            Rijndael myAlg = Rijndael.Create();
            myAlg.GenerateKey(); //secretkey
            myAlg.GenerateIV(); //iv

            var key = myAlg.Key;
            var iv = myAlg.IV;

            byte[] fileInBytes = clearFile.ToArray();
            var encryptedFile = SymmetricEncrypt(fileInBytes, iv, key);
            byte[] encryptedKey = AsymmetricEncrypt(key, publicKey);
            byte[] encryptedIv = AsymmetricEncrypt(iv, publicKey);

            MemoryStream msOut = new MemoryStream();
            msOut.Write(encryptedKey, 0, encryptedKey.Length);
            msOut.Write(encryptedIv, 0, encryptedIv.Length);

            MemoryStream encryptedFileContent = new MemoryStream(encryptedFile);
            encryptedFileContent.Position = 0;
            encryptedFileContent.CopyTo(msOut);

            return msOut;
        }

        public static MemoryStream HybridDecrypt(MemoryStream encryptedFile, string privateKey)
        {
            encryptedFile.Position = 0;
            var fileBytes = encryptedFile.GetBuffer();

            //Reading the encrypted key
            byte[] encKey = new byte[128];
            encryptedFile.Read(encKey, 0, 128);

            //Reading the encrypted IV
            byte[] encIv = new byte[128];
            encryptedFile.Read(encIv, 0, 128);

            byte[] key = AsymmetricDecrypt(encKey, privateKey);
            byte[] iv = AsymmetricDecrypt(encIv, privateKey);

            MemoryStream encFile = new MemoryStream();
            encryptedFile.CopyTo(encFile);

            byte[] encFileInBytes = encFile.ToArray();
            byte[] fileDecrypted = SymmetricHybridDecrypt(iv, key, encFileInBytes);

            MemoryStream originalFile = new MemoryStream(fileDecrypted);

            return originalFile;
        }

        public static string SignData(MemoryStream data)
        {
            RSACryptoServiceProvider myAlg = new RSACryptoServiceProvider();

            string privateKey = "<RSAKeyValue><Modulus>5pbqplI1t+JCj4klNpYqvB2zvdQC+S1iY4Zj5A3X8rCCDn8GNbn41lQJVBT78DdT3361T2Qbsv8vqamkVzjlgQqOL+/lPEJN+Z51T79iIxeDi+mI579QfeOeywqL7TAUBnf5jHhSCV3KiVzXkZ5HOBxLw6jZIioevPyUTHot9/E=</Modulus><Exponent>AQAB</Exponent><P>7azShyRcdzgEMi2jUfrgCmf+phR9Pv2qA/hveG14Q9NiLoohVtrv1LW9XEjqLGPOM3iI6YZFiNkuUFmHOZiMrw==</P><Q>+F49p/Cv0RINrknwdj3CzwCjDe0QEIoks4Px7KT6BIts1aII8OlHMjhhrfqZ0oufC1dFbTmnZM2v3vjC71ktXw==</Q><DP>SQvqkFpeiM2QjJN2NIX0QX6Axy5Y5/kyPZInQE30vnPDIyaU5IrZVvicQDawsf/iqfMLsSnxSQPmtg8t/keiRQ==</DP><DQ>W3kSCi06A607fqpatqGuguDALNvXo8/NDpSU4EwujMfw8Il584hnIVbkmtgGGSY1EE83EbA/N4ANuvgxi0dzzw==</DQ><InverseQ>iwFQd5MzIP/fTbyjf/Y5bT6kcnHRy9Pa5CiQcvcoAnDSXHS6tJCaLx2biCTihVq8m8/vQQQEl6AJBNRKPHmqeA==</InverseQ><D>tV+NsEdHw5yv2DD62WXitVbzk2PY9uAw3LHPjokpC9a0ZeyaGNZwGT2+nKloxbjvOwNyX8ERXIkGl4A9KPIy6f/xN5+04ZfZTIYPt7KgrSOsjf6OuZ7ljou7gbfLxtXMn5JxwUMJuqHh+6t6kwB70LDIGN17wHkQnGp8+k+lf9U=</D></RSAKeyValue>";

            myAlg.FromXmlString(privateKey);
            byte[] dataAsBytes = data.ToArray();
            byte[] digest = Hash(dataAsBytes);

            byte[] signature = myAlg.SignHash(digest, "SHA512");
            string sign = Convert.ToBase64String(signature);

            return sign;
        }

        public static bool VerifyData(MemoryStream data, string signature)
        {

            RSACryptoServiceProvider myAlg = new RSACryptoServiceProvider();

            string publicKey = "<RSAKeyValue><Modulus>5pbqplI1t+JCj4klNpYqvB2zvdQC+S1iY4Zj5A3X8rCCDn8GNbn41lQJVBT78DdT3361T2Qbsv8vqamkVzjlgQqOL+/lPEJN+Z51T79iIxeDi+mI579QfeOeywqL7TAUBnf5jHhSCV3KiVzXkZ5HOBxLw6jZIioevPyUTHot9/E=</Modulus><Exponent>AQAB</Exponent></RSAKeyValue>";
          
            myAlg.FromXmlString(publicKey);
            byte[] dataAsBytes = data.ToArray();
            byte[] digest = Hash(dataAsBytes);
            var signatureInBytes = Convert.FromBase64String(signature);

            bool valid =  myAlg.VerifyHash(digest, "SHA512", signatureInBytes);
            if (valid)
            {
                return valid;
            }
            else
            {
                return false;
            }
        }
    }
}