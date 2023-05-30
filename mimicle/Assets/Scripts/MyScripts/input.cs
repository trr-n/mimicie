using UnityEngine;

namespace Mimic.Extend
{
    public static class input
    {
        public static bool down() => Input.anyKeyDown;
        public static bool down(int mouse) => Input.GetMouseButtonDown(mouse);
        public static bool down(KeyCode key) => Input.GetKeyDown(key);
        public static bool down(string name) => Input.GetButtonDown(name);

        public static bool pressed() => Input.anyKey;
        public static bool pressed(int mouse) => Input.GetMouseButton(mouse);
        public static bool pressed(KeyCode key) => Input.GetKey(key);
        public static bool pressed(string name) => Input.GetButton(name);

        public static bool up(int mouse) => Input.GetMouseButtonUp(mouse);
        public static bool up(KeyCode key) => Input.GetKeyUp(key);
        public static bool up(string name) => Input.GetButtonUp(name);
    }
}