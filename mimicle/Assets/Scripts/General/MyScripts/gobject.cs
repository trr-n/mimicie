using UnityEngine;
using UnityEngine.UI;

namespace Self.Utils
{
    public enum Active { Self, Hierarchy }

    public static class Gobject
    {
        public static GameObject Generate(this GameObject[] g, Vector3 p = new(), Quaternion r = new())
        {
            int choice = Rnd.Choice(g);
            return MonoBehaviour.Instantiate(g[choice], p, r);
        }
        public static GameObject Generate(this GameObject g, Vector3 p = new(), Quaternion r = new())
        => MonoBehaviour.Instantiate(g, p, r);
        public static GameObject Generate(this GameObject gob) => MonoBehaviour.Instantiate(gob);

        public static bool Compare(this Collision info, string tag) => info.gameObject.CompareTag(tag);
        public static bool Compare(this Collider info, string tag) => info.CompareTag(tag);
        public static bool Compare(this Collision2D info, string tag) => info.gameObject.CompareTag(tag);
        public static bool Compare(this Collider2D info, string tag) => info.CompareTag(tag);

        public static T Get<T>(this Collision2D info) => info.gameObject.GetComponent<T>();
        public static T Get<T>(this Collider2D info) => info.gameObject.GetComponent<T>();
        public static T Get<T>(this Collision info) => info.gameObject.GetComponent<T>();
        public static T Get<T>(this Collider info) => info.gameObject.GetComponent<T>();

        public static bool Try<T>(this Collision2D info, out T t) => info.gameObject.TryGetComponent<T>(out t);
        public static bool Try<T>(this Collider2D info, out T t) => info.gameObject.TryGetComponent<T>(out t);
        public static bool Try<T>(this Collision info, out T t) => info.gameObject.TryGetComponent<T>(out t);
        public static bool Try<T>(this Collider info, out T t) => info.gameObject.TryGetComponent<T>(out t);
        public static bool Try<T>(this GameObject gob, out T t) => gob.TryGetComponent<T>(out t);

        public static GameObject Find(string tag) => GameObject.FindGameObjectWithTag(tag);
        public static GameObject[] Finds(string tag) => GameObject.FindGameObjectsWithTag(tag);

        public static void Destroy(this GameObject gob, float lifetime = 0) => UnityEngine.GameObject.Destroy(gob, lifetime);
        public static void Destroy(this Collider info, float lifetime = 0) => UnityEngine.GameObject.Destroy(info.gameObject, lifetime);
        public static void Destroy(this Collider2D info, float lifetime = 0) => UnityEngine.GameObject.Destroy(info.gameObject, lifetime);
        public static void Destroy(this Collision info, float lifetime = 0) => UnityEngine.GameObject.Destroy(info.gameObject, lifetime);
        public static void Destroy(this Collision2D info, float lifetime = 0) => UnityEngine.GameObject.Destroy(info.gameObject, lifetime);

        public static bool IsActive(this GameObject gob, Active? active = null)
        {
            if (active is null || active == Active.Self)
            {
                return gob.activeSelf;
            }
            return gob.activeInHierarchy;
        }
        public static bool IsActive(this Text text) => text.IsActive();
        public static bool Exist(this GameObject obj) => obj.gameObject;
    }
}
