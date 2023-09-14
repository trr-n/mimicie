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
            // ボス関連のものを非表示に
            bossRelated.SetActives(false);
        }

        void OnEnable()
        {
            data = transform.parent.gameObject.GetComponent<WaveData>();
        }

        void Update()
        {
            Spawn();

            // ぼすのHPが0になったら
            if (bossHP.IsZero)
            {
                // ゲームクリアのスイッチオン
                data.IsDone = true;
            }
        }

        readonly Runner bossActivate = new();
        void Spawn()
        {
            // ウェーブ2がアクティブじゃなかったらリターン
            if (!data.IsActiveWave(2)) return;

            // ボス関連のやつを表示する
            bossActivate.RunOnce(() => bossRelated.SetActives(true));
        }
    }
}
