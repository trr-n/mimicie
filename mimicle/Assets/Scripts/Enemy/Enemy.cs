using UnityEngine;
using MyGame.Utils;

namespace MyGame
{
    public abstract class Enemy : MonoBehaviour
    {
        /// <summary>
        /// 移動処理
        /// </summary>
        protected abstract void Move();

        /// <summary>
        /// 画面外にでたら破壊
        /// </summary>
        /// <param name="deadLine">ボーダー</param>
        protected virtual void Left(GameObject obj, float deadLine = -10.24f)
        {
            if (obj.transform.position.x <= deadLine)
            {
                Destroy(obj);
            }
        }

        /// <summary>
        /// 討伐数を増やして破壊する
        /// </summary>
        protected void AddSlainCountAndRemove(GameObject gob)
        {
            Slain slain = Gobject.Find(Constant.WaveManager).GetComponent<Slain>();
            slain.AddCount();
            Destroy(gob);
        }
    }
}
