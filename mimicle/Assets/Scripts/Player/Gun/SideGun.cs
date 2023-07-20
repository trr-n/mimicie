using UnityEngine;
using MyGame.Utils;

namespace MyGame
{
    public class SideGun : MonoBehaviour
    {
        [SerializeField]
        GameObject bullet;

        [SerializeField]
        Player player;

        /// <summary>
        /// 上下の銃
        /// </summary>
        GameObject[] guns = new GameObject[2];

        /// <summary>
        /// 連射用
        /// </summary>
        (float Rapid, Stopwatch sw) trigger = (0.2f, new());

        const int Max = 2;

        (int, bool, int) For(int init, bool boolean, int increase)
        {
            return (init, boolean, increase);
        }

        void Start()
        {
            for (int i = 0; i < Max; i++)
            {
                guns[i] = transform.GetChild(i).gameObject;
            }

            foreach (var i in guns)
            {
                i.SetActive(false);
            }
        }

        void Update()
        {
            Rotation();
        }

        void Rotation()
        {
            foreach (var i in guns)
            {
                i.transform.SetEuler(z: -90);
            }
        }

        void Fire()
        {
            if (trigger.sw.sf < trigger.Rapid || player.IsReloading)
            {
                return;
            }

            if (Mynput.Pressed(Values.Key.Fire))
            {
                trigger.sw.Restart();
            }
        }

        /// <summary>
        /// 上下の銃を追加
        /// </summary>
        public void Add(int hitCount)
        {
            switch (hitCount)
            {
                case 0:
                    break;

                case 1:
                    break;

                default:
                    throw new Eguitte("えぐい!ふやしすぎや!");
            }
        }
    }
}