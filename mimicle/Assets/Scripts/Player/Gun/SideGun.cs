using UnityEngine;
using Self.Utility;

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
        (float RapidSpeed, Stopwatch stopwatch) trigger = (0.2f, new(true));

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

        /// <summary>
        /// 発砲処理
        /// </summary>
        public void Shot()
        {
            foreach (var gun in sideguns)
            {
                // FIXME: 下の銃だけ撃てない
                if (gun.IsActive(Active.Self) && trigger.stopwatch.sf > trigger.RapidSpeed)
                {
                    print(gun.name + " is enable!");
                    bullet.Generate(gun.transform.position);
                    trigger.stopwatch.Restart();
                }
            }
        }

        /// <summary>
        /// 有効化
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