using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.IO;

namespace LocalPassBank
{
    public class Security
    {
        static private SHA512Cng sha512 = new SHA512Cng();
        static private SHA256Cng sha256 = new SHA256Cng();

        static public Byte[] ComputeHash512(String str)
        {
            sha512.Initialize();
            Byte[] computedStr = Encoding.Unicode.GetBytes(str);
            computedStr = sha512.ComputeHash(computedStr);
            return (computedStr);
        }

        static public Byte[] ComputeHash256(String str)
        {
            sha256.Initialize();
            Byte[] computedStr = Encoding.Unicode.GetBytes(str);
            computedStr = sha256.ComputeHash(computedStr);
            return (computedStr);
        }

        static public Byte[] Encryption(String str, Byte[] key)
        {
            Byte[] iv = new Byte[32];
            RNGCryptoServiceProvider rngCrypto = new RNGCryptoServiceProvider();
            rngCrypto.GetBytes(iv);
            RijndaelManaged crypto = new RijndaelManaged()
            {
                IV = iv,
                Key = key,
                BlockSize = 256,
                Mode = CipherMode.CBC,
                Padding = PaddingMode.ISO10126
            };
            ICryptoTransform encryptor = crypto.CreateEncryptor();
            MemoryStream memoryStream = new MemoryStream();
            CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write);
            cryptoStream.Write(Encoding.Unicode.GetBytes(str), 0, Encoding.Unicode.GetByteCount(str));
            cryptoStream.FlushFinalBlock();
            Byte[] value = iv.Concat(memoryStream.ToArray()).ToArray();
            cryptoStream.Close();
            memoryStream.Close();
            return (value);
        }

        static public String Decryption(Byte[] value, Byte[] key)
        {
            Byte[] iv = value.Take(32).ToArray();
            Byte[] toDecrypt = value.Skip(32).Take(value.Length - 32).ToArray();
            RijndaelManaged crypto = new RijndaelManaged()
            {
                IV = iv,
                Key = key,
                BlockSize = 256,
                Mode = CipherMode.CBC,
                Padding = PaddingMode.ISO10126
            };
            ICryptoTransform decryptor = crypto.CreateDecryptor();
            MemoryStream memoryStream = new MemoryStream(toDecrypt);
            CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);
            Byte[] decryptedString = new Byte[toDecrypt.Length];
            int decryptedByteCount = cryptoStream.Read(decryptedString, 0, decryptedString.Length);
            memoryStream.Close();
            cryptoStream.Close();
            return (Encoding.Unicode.GetString(decryptedString, 0, decryptedByteCount));
        }
    }
}
