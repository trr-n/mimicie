using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mimical.Extend;

namespace Mimical
{
    public class Horming0 : Bullet
    {
        int level = 0;
        int damage = 0;
        Boss boss;
        GameObject player;
        HP playerHp;
        bool escaping = true;

        void Start()
        {
            boss = GameObject.Find("boss 1").GetComponent<Boss>();
            level = boss.ActiveLevel;
            player = GameObject.FindGameObjectWithTag(Constant.Player);
            playerHp = player.GetComponent<HP>();
        }

        void Update()
        {
            Move(speed: 1);
            print($"php:{playerHp.Now}, dmg:{damage}");
            OutOfScreen(gameObject);
        }

        void OnCollisionEnter2D(Collision2D info)
        {
            if (info.Compare(Constant.Player))
                TakeDamage(info);
            if (info.Compare(Constant.Bullet))
            {
                info.gameObject.Remove();
                gameObject.Remove();
            }
        }

        Vector3 direction;
        float distance;
        bool b4 = true;
        protected override void Move(float speed)
        {
            switch (level)
            {
                case 1:
                    break;
                case 2:
                    damage = Numeric.Percent(playerHp.Now, 9);
                    break;
                case 3:
                    damage = Numeric.Percent(playerHp.Now, 11);
                    break;
                case 4:
                    damage = Numeric.Percent(playerHp.Now, 15);
                    distance = Vector3.Distance(transform.position, player.transform.position);
                    if (distance <= 7) escaping = false;
                    if (escaping) direction = (player.transform.position - transform.position).normalized;
                    else direction = transform.forward;
                    transform.Translate(direction * speed * Time.deltaTime);
                    break;
                case 0: default: damage = 0; break;
            }
        }

        protected override void TakeDamage(Collision2D info)
        {
            info.Get<HP>().Damage(Values.Damage.LilC);
            Score.Add(Values.Point.RedLilCBullet);
            gameObject.Remove();
        }
    }
}
