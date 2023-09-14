using UnityEngine;
using static Self.Game.Score;

namespace Self.Test
{
    public class Debugs : MonoBehaviour
    {
        void Update()
        {
            print("current time is " + CurrentTime);
        }
    }
}