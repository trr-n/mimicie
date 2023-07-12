using System.Text.Json;
using UnityEngine;
using Mimicle.Extend;
using System.IO;
using System.Text;

public class JsonFileEncryptSave : MonoBehaviour
{
    void Awake()
    {
        TestData data = new TestData
        {
            name = "huga",
            time = 123,
            score = 123
        };

        Save.Write(data, "hoge", Application.dataPath + "/test1.bin");
        Save.Read<TestData>(out var indie, "hoge", Application.dataPath + "/test1.bin");
        print($"name:{indie.name},time:{indie.time},score:{indie.score}");

        // Save.Write2(data, "huga", Application.dataPath + "/test2.bin");
        // Save.Read2<TestData>(out var indie2, "huga", Application.dataPath + "/test2.bin");
        // print($"name:{indie2.name},time:{indie2.time},score:{indie2.score}");

        // //! jsonに変換すると空になる
        // print(JsonSerializer.Serialize(data));
    }
}
