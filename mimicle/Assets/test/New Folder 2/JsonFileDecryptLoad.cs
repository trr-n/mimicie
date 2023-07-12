using System;
using System.Collections;
using UnityEngine;
using Mimicle.Extend;

public class JsonFileDecryptLoad : MonoBehaviour
{
    [SerializeField]
    JsonFileEncryptSave save;

    void Start()
    {
        // byte[] readData;
        // using (FileStream stream = new(Application.dataPath + "/saveJsonEncrypt.bin", FileMode.Open))
        // {
        //     readData = new byte[stream.Length];
        //     stream.Read(readData, 0, (int)stream.Length);
        // }

        // IEncryption encryption = new RijndaelEncryption("ODC");
        // var data = JsonUtility.FromJson<TestData>(encryption.DecryptToString(readData));
        var loadData = save.loadData;
        // print(save.loadData);
        Debug.Log("Name:" + loadData.name);
        Debug.Log("Time:" + loadData.time);
        Debug.Log("Score:" + loadData.score);
    }
    IEnumerator aa()
    {
        yield return null;
    }
}
