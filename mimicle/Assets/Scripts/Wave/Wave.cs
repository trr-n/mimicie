using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mimical.Extend;

namespace Mimical
{
    public class Wave : MonoBehaviour
    {
        int now = 0;
        public int Now => now;

        [SerializeField]
        List<GameObject> slainObjs;

        void Update()
        {
            if (input.Down(KeyCode.Space))
            {
                Next();
            }
        }

        public void Next()
        {
            now++;
        }
    }
}

/*
一定量雑魚を倒したらボスウェーブ突入
→敵倒したらリストに入れて指定数以上倒したら次のウェーブ(仮)
*/
