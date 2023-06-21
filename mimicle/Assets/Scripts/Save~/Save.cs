using System.Text;
using System.IO;
using UnityEngine;

namespace Mimical
{
    public class Save : MonoBehaviour
    {
        string path;
        SaveData data;

        void Awake()
        {
            // path = Application.persistentDataPath + "/" + ".savedata.json";
            path = Application.dataPath + "/" + @"\Scripts\test.json";
            data = new();
        }

        public void Write()
        {
            var sw = new StreamWriter(path);
            sw.Write(JsonUtility.ToJson(this.data));
            sw.Flush();
            sw.Close();
        }

        public void Load()
        {
            if (!File.Exists(path))
                return;
            var sr = new StreamReader(path, Encoding.UTF8);
            this.data = JsonUtility.FromJson<SaveData>(sr.ReadToEnd());
            sr.Close();
        }
    }
}
