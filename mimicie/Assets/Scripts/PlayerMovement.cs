using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mimic.Extend;

namespace Mimic
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField]
        GameObject test;

        // 反復11
        [Tooltip("移動距離")]
        float speed = 2f;

        new Rigidbody2D rigidbody;
        Quaternion selfq;
        Vector2 _;

        void Start()
        {
            Physics2D.gravity = new Vector3(0, 0, -9.81f);
            rigidbody = GetComponent<Rigidbody2D>();

            StartCoroutine(tf());
        }

        void Update()
        {
            var position = transform.position;
            position.x = Mathf.Clamp(transform.position.x, -2.5f, 2.5f);
            position.y = Mathf.Clamp(transform.position.y, -4.75f, 4.25f);
            transform.position = position;

            Move0();
        }

        void Move0()
        {
            float h = Input.GetAxis(constant.Horizontal), v = Input.GetAxis(constant.Vertical);
            Vector2 hv = h * Vector2.right + v * Vector2.up;
            transform.Translate(hv * speed * Time.deltaTime);
            hv.show();

            // if (hv.magnitude <= 0f)
            // {
            //     return;
            // }
            transform.rotation = Quaternion.FromToRotation(hv, Vector2.up);
        }

        IEnumerator tf()
        {
            while (true)
            {
                testf();
                "a".show();
                yield return new WaitForSeconds(1);
            }
        }

        void testf()
        {
            var a = test.ins(transform.position, Quaternion.identity);
            a.GetComponent<Rigidbody2D>().velocity = transform.up * 100;

            Destroy(a, 10);
        }

        void Move1()
        {
            if (input.down(KeyCode.A))
            {
                transform.Translate(Vector2.left * speed);
            }

            else if (input.down(KeyCode.D))
            {
                transform.Translate(Vector2.right * speed);
            }

            else if (input.down(KeyCode.W))
            {
                transform.Translate(Vector2.up * speed);
            }

            else if (input.down(KeyCode.S))
            {
                transform.Translate(Vector2.down * speed);
            }
        }
    }
}
