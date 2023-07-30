using System.Collections;
using System.Collections.Generic;
using Self.Utility;
using UnityEngine;

namespace Self
{
    [RequireComponent(typeof(AudioSource))]
    public class PlayerRocketLaucher : Bullet
    {
        [SerializeField]
        GameObject fireEffect, hitEffect;

        [SerializeField]
        AudioClip fireSound, hitSound;

        AudioSource speaker;

        Vector2 direction;
        (float basis, float reduction) speeds = (10, 0.5f);

        bool exploseable = false;

        void Start()
        {
            speaker = GetComponent<AudioSource>();
            //発砲音
            speaker.PlayOneShot(fireSound);

            direction = -transform.right;
        }

        void Update()
        {
            Move(speeds.basis);
            Explosion();
        }

        protected override void Move(float speed)
        {
            if (speed >= 0)
            {
                speed *= speeds.reduction;
            }
            else
            {
                exploseable = true;
            }
            transform.Translate(speed * direction * Time.deltaTime, Space.World);
        }

        void Explosion()
        {
            if (exploseable)
            {
                return;
            }
            print("ossoi bakuhatu surude-");
        }

        protected override void TakeDamage(Collision2D info)
        {
            info.Get<HP>().Damage(Values.Damage.PlayerRocket);
        }

        void OnCollisionEnter2D(Collision2D info)
        {
            if (info.Compare(Constant.Enemy))
            {
                TakeDamage(info);
            }
        }
    }
}