using System;
using System.IO;
using System.Text.Json;
using UnityEngine;

namespace Self.Utils
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
            using FileStream stream = new(path, fileMode);
            IEncryption encrypt = new RijndaelEncryption(password);
            byte[] dataArr = encrypt.Encrypt(JsonUtility.ToJson(data));
            stream.Write(dataArr, 0, dataArr.Length);
        }

        public static void Read<T>(out T read, string password, string path)
        {
            using FileStream stream = new(path, FileMode.Open);
            byte[] readArr = new byte[stream.Length];
            stream.Read(readArr, 0, (int)stream.Length);
            IEncryption decrypt = new RijndaelEncryption(password);
            read = JsonUtility.FromJson<T>(decrypt.DecryptToString(readArr));
        }

        public static T Read<T>(string password, string path)
        {
            Read<T>(out T data, password, path);
            return data;
        }
    }

    class Example
    {
        const string PW = "hoge";
        readonly string path = Application.dataPath + "huga.bin";

        struct SaveData { public string name; }
        SaveData data = new() { name = "hoge" };

        void Examplez()
        {
            Save.Write(data, PW, path);

            Save.Read<SaveData>(out var readData, PW, path);
            _ = readData.name;
        }
    }
}