using UnityEngine;
using Self.Utils;

namespace Self.Game
{
    public class HitParry : MonoBehaviour
    {
        [SerializeField]
        AudioClip parrySE;

        HP playerHP;

        new AudioSource audio;

        void Start()
        {
            audio = GetComponent<AudioSource>();
            playerHP = Gobject.GetWithTag<HP>(Constant.Player);
        }

        void OnCollisionEnter2D(Collision2D info)
        {
            if (info.Compare(Constant.EnemyBullet))
            {
                // 音鳴らす
                audio.PlayOneShot(parrySE);

                // 成功したら5%回復
                int amount = Numeric.Percent(playerHP.Max, 5);
                playerHP.Healing(amount);

                // タマ消す
                info.Destroy();
            }
        }
    }
}
