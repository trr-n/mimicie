using Self.Utils;
using UnityEngine;

namespace Self
{
    public class Wave3 : MonoBehaviour
    {
        [SerializeField]
        WaveData wdata;

        [SerializeField, Tooltip("0:charger\n1:lilc\n2:slilc\n3:spide")]
        GameObject[] mobs;

        [SerializeField]
        GameObject[] bossRelated;

        [SerializeField]
        GameObject boss;

        HP bossHP;

        Transform playerTransform;

        void Start()
        {
            bossRelated.SetActive(false);
        }

        void OnEnable()
        {
            playerTransform = Gobject.GetWithTag<Transform>(Constant.Player);
            bossHP = boss.GetComponent<HP>();
        }

        void Update()
        {
            Spawn();

            if (bossHP.IsZero)
            {
                wdata.IsDone = true;
            }
        }

        Runtime bossActivate = new();
        void Spawn()
        {
            if (wdata.Now != 3)
            {
                return;
            }

            bossActivate.RunOnce(() => bossRelated.SetActive(true));
        }
    }
}
