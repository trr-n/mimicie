using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mimical.Rubbish
{
    public class WaveData : MonoBehaviour
    {
        enum Status { None, Exist }
        [SerializeField]
        Status endType = Status.None;

        public bool IsEnd() => !(endType == Status.Exist && gameObject);
    }
}
