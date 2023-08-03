using UnityEngine;

namespace Self
{
    public class _Values : MonoBehaviour
    {
        public static class Damage
        {
            public static int[] Player = { 25, 30, 35 };
            public static int PlayerRocket = 33;
            // public static int MiniPlayer = 5;
            public static int Charger = 5;
            public static int LilC = 10;
            public static int BigC = 5;
            public static int Spide = 24;
            public static int Shuriken = 15;
            public static int Ring = 49;
        }

        public static class Point
        {
            public static int Charger = 100;
            public static int LilC = 500;
            public static int BigC = 500;
            public static int Boss = 10000;
            public static int Spide = 250;
            public static int Ninja = 400;

            public static int RedCharger = -10;
            public static int RedLilC = -100;
            public static int RedLilCBullet = -10;
            public static int RedBigCBullet = -10;
            public static int RedHoming = -10;
            public static int RedSpide = -50;
            public static int RedShuriken = -10;
        }

        public static class Key
        {
            public static KeyCode Reload = KeyCode.LeftShift;
            public static KeyCode Fire = KeyCode.Space;
            public static KeyCode Mute = KeyCode.M;
            public static KeyCode Stop = KeyCode.Backspace;
            public static KeyCode VUp = KeyCode.UpArrow;
            public static KeyCode VDown = KeyCode.DownArrow;
            public static KeyCode MChange = KeyCode.LeftArrow;
            public static KeyCode Parry = KeyCode.E;
        }
    }
}
