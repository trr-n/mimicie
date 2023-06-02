using UnityEngine;

namespace Mimical.Extend
{
    public static class gobject
    {
        public static GameObject Instance(
            this GameObject[] gobjects, Vector3 position, Quaternion rotation)
        => UnityEngine.MonoBehaviour.Instantiate(
            gobjects[random.ice(gobjects.Length)], position, rotation);

        public static GameObject Instance(
            this GameObject gobject, Vector3 position, Quaternion rotation)
        => UnityEngine.MonoBehaviour.Instantiate(gobject, position, rotation);

        public static GameObject Instance(this GameObject gob)
        => UnityEngine.MonoBehaviour.Instantiate(gob);

        public static bool Exist(this GameObject gobject) => gobject;

        public static bool Compare(this Collision info, string tag)
        => info.gameObject.CompareTag(tag);

        public static bool Compare(this Collider info, string tag)
        => info.CompareTag(tag);

        public static bool Compare(this Collision2D info, string tag)
        => info.gameObject.CompareTag(tag);

        public static bool Compare(this Collider2D info, string tag)
        => info.CompareTag(tag);

        public static bool Try<T>(this Collision info, out T t)
        => info.gameObject.TryGetComponent<T>(out t);

        public static bool Try<T>(this GameObject gob, out T t)
        => gob.TryGetComponent<T>(out t);

        public static GameObject Find(string tag)
        => GameObject.FindGameObjectWithTag(tag);

        public static GameObject[] Finds(string tag)
        => GameObject.FindGameObjectsWithTag(tag);

        public static void Remove(this GameObject gob, float lifetime = 0)
        => UnityEngine.GameObject.Destroy(gob, lifetime);

        public static void Remove(this Collider info, float lifetime = 0)
        => UnityEngine.GameObject.Destroy(info.gameObject, lifetime);

        public static void Remove(this Collision info, float lifetime = 0)
        => UnityEngine.GameObject.Destroy(info.gameObject, lifetime);
    }
}
