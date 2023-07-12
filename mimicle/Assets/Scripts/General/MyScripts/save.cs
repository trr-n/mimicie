using System;
using System.IO;
using UnityEngine;

namespace Mimicle.Extend
{
    public class Save
    {
        string password;
        string path;

        public Save(string password, string path)
        {
            this.password = password;
            this.path = path;
        }

        public void Write(object data)
        {
            var encrypt = new RijndaelEncryption(this.password);
            var hexData = encrypt.Encrypt(JsonUtility.ToJson(data));
            using (FileStream stream = new(path, FileMode.Create))
            {
                stream.Write(hexData, 0, hexData.Length);
            }
        }

        public T Read<T>()
        {
            byte[] read;
            using (FileStream stream = new(path, FileMode.Open))
            {
                read = new byte[stream.Length];
                stream.Read(read, 0, ((int)stream.Length));
            }
            var decrypt = new RijndaelEncryption(this.password);
            return JsonUtility.FromJson<T>(decrypt.DecryptToString(read));
        }
    }
}