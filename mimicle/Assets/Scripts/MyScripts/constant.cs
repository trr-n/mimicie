using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mimical
{
    public static class cst
    {
        public enum sindex
        {
            Title = 0,
        }

        public static readonly string
        //------------------------------------------------------------------------------
        // SCENE NAME
        //------------------------------------------------------------------------------
        Main = "Main";

        //------------------------------------------------------------------------------
        // KEY NAME?
        //------------------------------------------------------------------------------
        public static readonly string
        Horizontal = "Horizontal",
        Vertical = "Vertical",
        Fire = "Fire",
        Reload = "Reload",
        Jump = "Jump",
        MouseX = "Mouse X",
        MouseY = "Mouse Y",
        Volume = "Volume";

        // public static readonly KeyCode
        // Reload = KeyCode.LeftShift;

        //------------------------------------------------------------------------------
        // TAG NAME
        //------------------------------------------------------------------------------
        public static readonly string
        Player = "Player",
        Enemy = "Enemy",
        Safety = "LifeZone";

    }
}

// Space || A = Fire,
// LShift || B = Reload,
// WASD || LStick = Moving
