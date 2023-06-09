using UnityEngine;

namespace Mimical.Extend
{
    public enum c { hide, show }

    public enum v { inscene = CursorLockMode.Confined, locked = CursorLockMode.Locked, unlocked = CursorLockMode.None }

    public static class visual
    {
        public static float Fps() => Mathf.Floor(1 / Time.deltaTime);

        public static void Cursor(c status = c.hide, v clock = v.inscene)
        {
            UnityEngine.Cursor.visible = status != c.hide;

            UnityEngine.Cursor.lockState = (CursorLockMode)clock;
        }
    }
}
