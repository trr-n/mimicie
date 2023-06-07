using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Mimical.Extend
{
    public enum Active { Self, Hierarchy }

    public static class gobject
    {
        public static GameObject Instance(
            this GameObject[] gobjects, Vector3 position, Quaternion rotation)
        => UnityEngine.MonoBehaviour.Instantiate(
            gobjects[random.ice(gobjects)], position, rotation);

        public static GameObject Instance(
            this GameObject gobject, Vector3 position, Quaternion rotation)
        => UnityEngine.MonoBehaviour.Instantiate(gobject, position, rotation);

        public static GameObject Instance(this GameObject gob)
        => UnityEngine.MonoBehaviour.Instantiate(gob);


        public static bool Compare(this Collision info, string tag)
        => info.gameObject.CompareTag(tag);

        public static bool Compare(this Collider info, string tag)
        => info.CompareTag(tag);

        public static bool Compare(this Collision2D info, string tag)
        => info.gameObject.CompareTag(tag);

        public static bool Compare(this Collider2D info, string tag)
        => info.CompareTag(tag);


        public static T Get<T>(this Collision2D info)
        => info.gameObject.GetComponent<T>();

        public static T Get<T>(this Collider2D info)
        => info.gameObject.GetComponent<T>();

        public static T Get<T>(this Collision info)
        => info.gameObject.GetComponent<T>();

        public static T Get<T>(this Collider info)
        => info.gameObject.GetComponent<T>();


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


        public static bool isActive(this GameObject gob, Active active = Active.Hierarchy)
        {
            switch (active)
            {
                case Active.Self:
                    return gob.activeSelf;

                case Active.Hierarchy:
                    return gob.activeInHierarchy;

                default:
                    throw new System.Exception();
            }
        }

        public static bool isActive(this Text text) => text.IsActive();
    }
}
