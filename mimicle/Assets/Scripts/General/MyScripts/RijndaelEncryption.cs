using System.Text;
using System.Collections.Generic;
using System.Security.Cryptography;

namespace Mimicle.Extend
{
    public class RijndaelEncryption : IEncryption
    {
        readonly string password;
        readonly (int bufferKey, int block, int key) size;

        public RijndaelEncryption(
            string password_, int bufferKey = 32, int blockSize = 256, int keySize = 256)
        {
            this.password = password_;
            this.size.bufferKey = bufferKey;
            this.size.block = blockSize;
            this.size.key = keySize;
        }

        public byte[] Encrypt(byte[] src)
        {
            RijndaelManaged rijndaelManaged = new RijndaelManaged();
            rijndaelManaged.BlockSize = size.block;
            rijndaelManaged.KeySize = size.key;
            rijndaelManaged.Mode = CipherMode.CBC;
            rijndaelManaged.Padding = PaddingMode.PKCS7;

            Rfc2898DeriveBytes deriveBytes = new Rfc2898DeriveBytes(password, size.bufferKey);
            byte[] salt = deriveBytes.Salt;
            rijndaelManaged.Key = deriveBytes.GetBytes(size.bufferKey);
            rijndaelManaged.GenerateIV();

            using (ICryptoTransform encrypt = rijndaelManaged.CreateEncryptor(rijndaelManaged.Key, rijndaelManaged.IV))
            {
                byte[] dest = encrypt.TransformFinalBlock(src, 0, src.Length);
                List<byte> compile = new List<byte>(salt);
                compile.AddRange(rijndaelManaged.IV);
                compile.AddRange(dest);
                return compile.ToArray();
            }
        }

        public byte[] Encrypt(string src) => Encrypt(Encoding.UTF8.GetBytes(src));

        public byte[] Decrypt(byte[] src)
        {
            RijndaelManaged rijndaelManaged = new RijndaelManaged();
            rijndaelManaged.BlockSize = size.block;
            rijndaelManaged.KeySize = size.key;
            rijndaelManaged.Mode = CipherMode.CBC;
            rijndaelManaged.Padding = PaddingMode.PKCS7;

            List<byte> compile = new List<byte>(src);
            List<byte> salt = compile.GetRange(0, size.bufferKey);
            rijndaelManaged.IV = compile.GetRange(size.bufferKey, size.bufferKey).ToArray();

            Rfc2898DeriveBytes deriveBytes = new Rfc2898DeriveBytes(password, salt.ToArray());
            rijndaelManaged.Key = deriveBytes.GetBytes(size.bufferKey);

            byte[] plain = compile.GetRange(size.bufferKey * 2, compile.Count - (size.bufferKey * 2)).ToArray();
            using (ICryptoTransform decrypt = rijndaelManaged.CreateDecryptor(rijndaelManaged.Key, rijndaelManaged.IV))
            {
                return decrypt.TransformFinalBlock(plain, 0, plain.Length);
            }
        }

        public string DecryptToString(byte[] src) => Encoding.UTF8.GetString(Decrypt(src));
    }
}