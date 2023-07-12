using UnityEngine;
using Mimicle.Extend;

public class JsonFileEncryptSave : MonoBehaviour
{
    [System.NonSerialized]
    public TestData loadData;
    void Awake()
    {
        var data = new TestData("cet", 123, 456);
        Save save = new("hoge", Application.dataPath + "/test.bin");
        // save.Write(data);
        loadData = save.Read<TestData>();
        // print(loadData.name);
        // print(loadData.time);
        // print(loadData.score);
    }
}
