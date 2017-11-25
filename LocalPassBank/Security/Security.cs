using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.IO;
using System.Windows;

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
            Byte[] data = Encoding.Unicode.GetBytes(str);
            Byte[] iv = new Byte[16];
            RNGCryptoServiceProvider rngCrypto = new RNGCryptoServiceProvider();
            rngCrypto.GetBytes(iv);
            RijndaelManaged crypto = new RijndaelManaged()
            {
                IV = iv,
                Key = key
            };
            MemoryStream memoryStream = new MemoryStream();
            CryptoStream cryptoStream = new CryptoStream(memoryStream, crypto.CreateEncryptor(), CryptoStreamMode.Write);
            cryptoStream.Write(data, 0, data.Length);
            cryptoStream.FlushFinalBlock();
            Byte[] value = iv.Concat(memoryStream.ToArray()).ToArray();
            cryptoStream.Close();
            memoryStream.Close();
            return (value);
        }

        static public String Decryption(Byte[] value, Byte[] key)
        {
            if (value.Length == 0)
                return (String.Empty);
            Byte[] iv = value.Take(16).ToArray();
            Byte[] toDecrypt = value.Skip(16).Take(value.Length - 16).ToArray();
            RijndaelManaged crypto = new RijndaelManaged()
            {
                IV = iv,
                Key = key
            };
            MemoryStream memoryStream = new MemoryStream();
            CryptoStream cryptoStream = new CryptoStream(memoryStream, crypto.CreateDecryptor(), CryptoStreamMode.Write);
            cryptoStream.Write(toDecrypt, 0, toDecrypt.Length);
            cryptoStream.FlushFinalBlock();
            Byte[] decryptedString = memoryStream.ToArray();
            memoryStream.Close();
            cryptoStream.Close();
            String str = Encoding.Unicode.GetString(decryptedString);
            return (str);
        }
    }
}
