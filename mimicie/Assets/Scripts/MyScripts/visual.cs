using UnityEngine;

namespace Mimic.Extend
{
    public enum c { hide, show }
    public enum v { inscene = CursorLockMode.Confined, locked = CursorLockMode.Locked, unlocked = CursorLockMode.None }
    public static class visual
    {
        public static float fps() => Mathf.Floor(1 / Time.deltaTime);

        public static string timer(int digits) => Time.time.ToString("F" + digits);

        public static void cursor(c status, v clock = v.inscene)
        {
            Cursor.visible = status != c.hide;
            Cursor.lockState = (CursorLockMode)clock;
        }

        public static bool timer(this float timer, float limit) => timer < limit;
    }
}
