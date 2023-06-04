using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mimical.Rubbish
{
    public class Wave : MonoBehaviour
    {
        [System.Serializable]
        struct Data
        {
            public int frame;
            public WaveData waveObj;
        }
        [SerializeField]
        List<Data> objList;

        int nowObj = 0;
        int frame = 0;
        bool waveEnd = false;

        void Start()
        {
            for (var i = 0; i < objList.Count; i++)
                if (objList[i].waveObj)
                    objList[i].waveObj.gameObject.SetActive(false);
        }

        void Update()
        {
            if (objList.Count <= 0 || waveEnd)
                return;
            frame++;
            while (frame >= objList[nowObj].frame)
            {
                if (objList[nowObj].waveObj)
                    objList[nowObj].waveObj.gameObject.SetActive(true);
                NextObj();
                if (waveEnd)
                    break;
            }
        }

        void NextObj()
        {
            if (nowObj < objList.Count - 1)
            {
                nowObj++;
                frame = 0;
            }
            else
                waveEnd = true;
        }

        public bool IsEnd()
        {
            for (int i = 0; i < objList.Count; i++)
                if (objList[i].waveObj && !objList[i].waveObj.IsEnd())
                    return false;
            return waveEnd;
        }

        public bool IsDelete()
        {
            for (int i = 0; i < objList.Count; i++)
                if (objList[i].waveObj && objList[i].waveObj.gameObject)
                    return false;
            return true;
        }
    }
}
