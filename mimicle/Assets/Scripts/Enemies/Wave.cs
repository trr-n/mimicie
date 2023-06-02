using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mimical
{
    public class Wave : MonoBehaviour
    {
        int current = 0;
        public int Current => current;

        public void Add()
        {
            current++;
        }
    }
}
