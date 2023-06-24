using UnityEngine;

namespace Mimical
{
    public class Slain : MonoBehaviour
    {
        int count = 0;

        public int Count => count;

        void Awake() => ResetCount();

        public void ResetCount() => count = 0;

        public void AddCount() => count++;
    }
}
