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
        bool chasing = true;
        int[] pers = { 0, 7, 9, 11, 15 };
        SpriteRenderer sr;

        void Start()
        {
            boss = GameObject.Find("boss 1").GetComponent<Boss>();
            level = boss.ActiveLevel;
            player = GameObject.FindGameObjectWithTag(Constant.Player);
            playerHp = player.GetComponent<HP>();
        }

        void Update()
        {
            Move(speed: 12);
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

        Quaternion rotate;
        // Vector3 direction;
        float distance;
        protected override void Move(float speed)
        {
            switch (level)
            {
                case 1:
                    damage = Numeric.Percent(playerHp.Now, pers[1]);
                    sr.color = new(0.21f, 0.98f, 0.4f);
                    break;
                case 2:
                    damage = Numeric.Percent(playerHp.Now, pers[2]);
                    sr.color = new(0.87f, 0.98f, 0.21f);
                    break;
                case 3:
                    damage = Numeric.Percent(playerHp.Now, pers[3]);
                    sr.color = new(0.98f, 0.66f, 0.21f);
                    break;
                case 4:
                    damage = Numeric.Percent(playerHp.Now, pers[4]);
                    sr.color = new(0.98f, 0.21f, 0.6f);
                    distance = Vector3.Distance(transform.position, player.transform.position);
                    if (distance <= 5) chasing = false;
                    rotate = chasing ?
                        Quaternion.FromToRotation(Vector2.up, player.transform.position - transform.position) :
                        Quaternion.Euler(0, 0, transform.eulerAngles.z);
                    transform.rotation = new(0, 0, rotate.z, rotate.w);
                    transform.Translate(Vector2.up * speed * Time.deltaTime);
                    break;
                case 0: default: damage = pers[0]; break;
            }
        }

        protected override void TakeDamage(Collision2D info)
        {
            info.Get<HP>().Damage(damage);
            Score.Add(pers[4] * -2);
            gameObject.Remove();
        }
    }
}
