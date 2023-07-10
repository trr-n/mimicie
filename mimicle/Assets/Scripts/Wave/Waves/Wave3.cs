using Mimical.Extend;
using UnityEngine;

namespace Mimical
{
    public class Wave3 : MonoBehaviour
    {
        [SerializeField]
        WaveData data;
        [SerializeField, Tooltip("0:charger\n1:lilc\n2:slilc\n3:spide")]
        GameObject[] mobs;
        [SerializeField]
        GameObject[] bossRelated;
        [SerializeField]
        GameObject boss;

        Transform playerTransform;

        void Start()
        {
            foreach (var i in bossRelated)
            {
                i.SetActive(false);
            }
        }

        void OnEnable()
        {
            playerTransform = GameObject.FindGameObjectWithTag(Constant.Player).transform;
        }

        void Update()
        {
            Spawn();
            if (boss.GetComponent<HP>().IsZero)
            {
                data.IsDone = true;
                Score.StopTimerFinal();
            }
        }

        One one = new();
        void Spawn()
        {
            if (data.Now != 3)
            {
                return;
            }
            transform.position = new();
            one.Once(() =>
            {
                foreach (var i in bossRelated)
                {
                    i.SetActive(true);
                }
            });
        }
    }
}
