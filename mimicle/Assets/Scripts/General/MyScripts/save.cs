using System.IO;
using System.Text.Json;
using UnityEngine;

namespace Mimicle.Extend
{
    public class Save
    {
        public static void Write(object data, string password, string path)
        {
            using (FileStream stream = new(path, FileMode.Create))
            {
                var encrypt = new RijndaelEncryption(password);
                var dataArr = encrypt.Encrypt(JsonUtility.ToJson(data));
                stream.Write(dataArr, 0, dataArr.Length);
            }
        }

        public static void Read<T>(out T read, string password, string path)
        {
            using (FileStream stream = new(path, FileMode.Open))
            {
                byte[] readArr = new byte[stream.Length];
                stream.Read(readArr, 0, ((int)stream.Length));
                var decrypt = new RijndaelEncryption(password);
                read = JsonUtility.FromJson<T>(decrypt.DecryptToString(readArr));
            }
        }

        [System.Obsolete]
        public static void Write2(object data, string password, string path)
        {
            using (FileStream stream = new(path, FileMode.Create))
            {
                var encrypt = new RijndaelEncryption(password);
                var hexData = encrypt.Encrypt(JsonSerializer.Serialize(data));
                stream.Write(hexData, 0, hexData.Length);
            }
        }

        [System.Obsolete]
        public static void Read2<T>(out T read, string password, string path)
        {
            using (FileStream stream = new(path, FileMode.Open))
            {
                byte[] readArr = new byte[stream.Length];
                stream.Read(readArr, 0, ((int)stream.Length));
                var decrypt = new RijndaelEncryption(password);
                read = JsonSerializer.Deserialize<T>(decrypt.DecryptToString(readArr));
            }
        }

    }

    class Example
    {
        struct SaveData { public string name; }

        void Examples()
        {
            // write //
            Save.Write(new SaveData { name = "hoge" }, "hoge", Application.dataPath + "huga.bin");

            // read //
            Save.Read<SaveData>(out var data, "hoge", Application.dataPath + "huga.bin");
            _ = data.name;
        }
    }
}