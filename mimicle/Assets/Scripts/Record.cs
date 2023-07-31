using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Self.Utility;

namespace System.Runtime.CompilerServices
{
    internal static class IsExternalInit { }
}

public class Record : MonoBehaviour
{
    record SaveData(string name, int time, int score);
    // {
    //     public string name { get; init; }
    //     public int time { get; init; }
    //     public int score { get; init; }
    // }

    struct SaveData2
    {
        public string name;
        public int time;
        public int score;
    }

    void Start()
    {
        SaveData data = new("hoge", 123, 1233);
        // SaveData data = new SaveData { name = "hoge", time = 123, score = 1233 };
        string path = Application.dataPath + "/aaaaa.bin";
        Save.Write(data, "huga", path);
        Save.Read<SaveData>(out var read, "huga", path);
        //! null
        print(read.name);
        print(read.time);
        print(read.score);

        SaveData2 data2 = new SaveData2 { name = "hoge2", time = 112233, score = 1122333 };
        string path2 = Application.dataPath + "/aaaaa2.bin";
        Save.Write(data2, "a", path2);
        Save.Read<SaveData2>(out var read2, "a", path2);
        print(read2.name);
        print(read2.time);
        print(read2.score);
    }

    enum Status { A, B, C }
    int n = 0;
    void Update()
    {
        if (Feed.Down(KeyCode.Alpha1))
            n = 0;
        else if (Feed.Down(KeyCode.Alpha2))
            n = 1;
        else if (Feed.Down(KeyCode.Alpha3))
            n = 2;
        else if (Feed.Down(KeyCode.Alpha4))
            n = 3;

        Status status = n switch
        {
            0 => Status.A,
            1 => Status.B,
            2 => Status.C,
            _ => throw new Karappoyanke()
        };
        print(status);
    }
}