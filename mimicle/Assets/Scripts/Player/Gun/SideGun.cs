using UnityEngine;
using Self.Utils;

namespace Self
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
        GameObject[] sideguns = new GameObject[2];

        /// <summary>
        /// 連射用
        /// </summary>
        (float Rapid, Stopwatch stopwatch) trigger = (0.2f, new(true));

        const int Max = 2;

        void Start()
        {
            // 銃を無効化
            for (int index = 0; index < Max; index++)
            {
                sideguns[index] = transform.GetChild(index).gameObject;
                if (sideguns[index] is not null)
                {
                    sideguns[index].SetActive(false);
                }
            }
        }

        void _Update()
        {
        }

        public void Shot()
        {
            foreach (var gun in sideguns)
            {
                if (gun.IsActive(Active.Self) && trigger.stopwatch.sf > trigger.Rapid)
                {
                    print(gun.name + " is enable!");
                    bullet.Generate(gun.transform.position);
                    trigger.stopwatch.Restart();
                }
            }
        }

        /// <summary>
        /// 上下の銃を追加
        /// </summary>
        public void Add(int hitCount)
        {
            if (hitCount < 1 && hitCount > 2)
            {
                return;
            }

            switch (hitCount)
            {
                case 0:
                    sideguns[0].SetActive(true);
                    break;

                case 1:
                    sideguns[1].SetActive(true);
                    break;

                default:
                    throw new Eguitte("ふやしすぎや!");
            }
        }
    }
}