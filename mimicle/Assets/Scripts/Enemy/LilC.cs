using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mimical.Extend;

namespace Mimical
{
    public class LilC : Enemy
    {
        [SerializeField]
        GameObject bullet;

        HP hp;

        float speed = 1;

        Vector2 direction;
        Vector2 position;
        Vector2 firstPosition = new(5, 0);

        bool isEndAttack = false;

        int fireCount = 0;
        float timer = 0;
        float rapid = 2;

        void Start()
        {
            hp = GetComponent<HP>();
            base.Start(hp);

            position = transform.position;
            direction = firstPosition - position;
        }

        void Update()
        {
            Move();
            Dead(gameObject, hp);
        }

        protected override void Move()
        {
            // 5, 0 に移動
            if (transform.position.x >= firstPosition.x)
                transform.Translate(direction * speed * Time.deltaTime);

            // プレイヤーに向かって2発発砲
            timer += Time.deltaTime;

            if (!isEndAttack && timer <= rapid)
            {
                "lil was fired".show();

                var fireDirection = gobject.Find(Const.Player).transform.position;
                bullet.Instance(transform.position + new Vector3(0.75f, 0), Quaternion.identity);

                fireCount++;
                timer = 0;
            }

            // 2発撃ったら発砲終了
            // TODO 左に突進、画面外に出たら破壊
            if (fireCount >= 2)
            {
                isEndAttack = true;
            }
        }
    }
}
