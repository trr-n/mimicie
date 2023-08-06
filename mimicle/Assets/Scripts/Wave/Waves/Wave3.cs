using Self.Utils;
using UnityEngine;

namespace Self.Game
{
    public class Wave3 : MonoBehaviour
    {
        [SerializeField]
        [Tooltip("0:charger\n1:lilc\n2:slilc\n3:spide")]
        GameObject[] mobs;

        [SerializeField]
        GameObject[] bossRelated;

        [SerializeField]
        HP bossHP;

        WaveData data;

        void Start()
        {
            bossRelated.SetActives(false);
        }

        void OnEnable()
        {
            data = transform.parent.gameObject.GetComponent<WaveData>();
        }

        void Update()
        {
            Spawn();

            if (bossHP.IsZero)
            {
                data.IsDone = true;
            }
        }

        readonly Runner bossActivate = new();
        void Spawn()
        {
            if (!data.IsActiveWave(2))
            {
                return;
            }

            bossActivate.RunOnce(() => bossRelated.SetActives(true));
        }
    }
}
