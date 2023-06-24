using UnityEngine;
using UnityEngine.UI;

namespace Mimical
{
    public class BossUI : MonoBehaviour
    {
        [SerializeField]
        GameObject core;

        [SerializeField]
        Image gauge;

        [SerializeField]
        Text text;

        [SerializeField]
        HP bossHp;

        [SerializeField]
        EnemySpawner spawner;

        bool once = false;

        void Start()
        {
            if (spawner.StartWave3 && !once)
            {
                // bossHp = gobject.Find(constant.Boss).GetComponent<HP>();
                once = true;
            }
        }

        public void UpdateBossUI()
        {
            gauge.fillAmount = bossHp.Ratio;
            text.text = bossHp.Now.ToString();
        }
    }
}
