using System;
using System.IO;
using System.Text.Json;
using UnityEngine;

namespace Self.Utility
{
    public sealed class Save
    {
        readonly string path;
        readonly string password;
        // Type type;

        [Obsolete]
        public Save(string path, string password)
        {
            this.path = path;
            this.password = password;
        }

        [Obsolete]
        public void Write(object data) => Write(data, this.password, this.path);

        [Obsolete]
        public T Read<T>()
        {
            Read<T>(out T read, this.password, this.path);
            return read;
        }

        public static void Write(object data, string password, string path, FileMode fileMode = FileMode.Create)
        {
            using (FileStream stream = new(path, fileMode))
            {
                IEncryption encrypt = new RijndaelEncryption(password);
                byte[] dataArr = encrypt.Encrypt(JsonUtility.ToJson(data));
                stream.Write(dataArr, 0, dataArr.Length);
            }
        }

        public static void Read<T>(out T read, string password, string path)
        {
            using (FileStream stream = new(path, FileMode.Open))
            {
                byte[] readArr = new byte[stream.Length];
                stream.Read(readArr, 0, ((int)stream.Length));
                IEncryption decrypt = new RijndaelEncryption(password);
                read = JsonUtility.FromJson<T>(decrypt.DecryptToString(readArr));
            }
        }
    }

    class Example
    {
        const string PW = "hoge";
        string path = Application.dataPath + "huga.bin";

        struct SaveData { public string name; }
        SaveData data = new SaveData { name = "hoge" };

        void Examplez()
        {
            // instance
            Save save = new(path, PW);
            save.Write(this.data);

            var data = save.Read<SaveData>();
            _ = data.name;

            // static
            Save.Write(data, PW, path);

            Save.Read<SaveData>(out var readData, PW, path);
            _ = readData.name;
        }
    }
}