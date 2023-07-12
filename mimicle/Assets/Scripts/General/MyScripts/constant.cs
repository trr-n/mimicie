using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mimicle
{
    public static class Constant
    {
        public enum sindex
        {
            Title = 0,
            Main = 1
        }

        public static readonly string
        Main = "Main",
        Title = "Title",
        Final = "Final";

        public static readonly string
        Horizontal = "Horizontal",
        Vertical = "Vertical",
        Fire = "Fire",
        Reload = "Reload",
        Jump = "Jump",
        MouseX = "Mouse X",
        MouseY = "Mouse Y",
        Volume = "Volume";

        public static readonly string
        Player = "Player",
        Enemy = "Enemy",
        Safety = "LifeZone",
        Dark = "Dark",
        Manager = "Manager",
        Charger = "Charger",
        LilC = "LilC",
        Bullet = "Bullet",
        Logo = "Logo",
        Boss = "Boss",
        TriggerZone = "TriggerArea",
        EnemyBullet = "EnemyBullet",
        WaveManager = "WaveManager";
    }
}
