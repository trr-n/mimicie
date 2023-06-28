using UnityEngine;
using static UnityEngine.Cursor;

namespace Mimical.Extend
{
    public enum c { hide, show }
    public enum v { inscene = CursorLockMode.Confined, locked = CursorLockMode.Locked, unlocked = CursorLockMode.None }
    public static class Render
    {
        public static float Fps() => Mathf.Floor(1 / Time.deltaTime);
        public static void Cursor(c status = c.hide, v clock = v.inscene) { visible = status != c.hide; lockState = (CursorLockMode)clock; }
    }
}
