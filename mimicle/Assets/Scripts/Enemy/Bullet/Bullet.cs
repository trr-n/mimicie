using UnityEngine;
using Self.Utils;

namespace Self.Game
{
    public abstract class Bullet : MonoBehaviour
    {
        /// <summary>
        /// 移動処理
        /// </summary>
        /// <param name="speed">移動速度</param>
        protected abstract void Move(float speed);

        /// <summary>
        /// ダメージ処理
        /// </summary>
        protected abstract void TakeDamage(Collision2D info);

        /// <summary>
        /// 画面外に出たら破壊
        /// </summary>
        protected virtual void OutOfScreen(GameObject gobject)
        {
            (float x, float y) border = (12.80f, 7.20f);
            if (gobject.transform.position.x > border.x || gobject.transform.position.x < -border.x ||
                gobject.transform.position.y > border.y || gobject.transform.position.y < -border.y)
            {
                Destroy(gameObject);
            }
        }
    }
}
