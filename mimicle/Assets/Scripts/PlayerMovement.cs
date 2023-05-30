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
            rigidbody = GetComponent<Rigidbody2D>();

            Physics2D.gravity = new Vector3(0, 0, -9.81f);
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
            float h = Input.GetAxis(cst.Horizontal), v = Input.GetAxis(cst.Vertical);
            Vector2 hv = h * Vector2.right + v * Vector2.up;
            transform.Translate(hv * speed * Time.deltaTime);
            // rigidbody.velocity = hv * speed * Time.deltaTime;

            // if (hv.magnitude <= 0f)
            // {
            //     return;
            // }
            // transform.rotation = Quaternion.FromToRotation(hv, Vector2.up);
        }

        void GetMovingCursorDirection(int click = 0)
        {
            Vector2 preCursorPos = new();
            Vector2 currentCursorPos = Input.mousePosition;
            Vector2 direction;

            if (input.down(click))
            {
                preCursorPos = Input.mousePosition;
            }

            if (input.up(click))
            {
                direction = preCursorPos - currentCursorPos;
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
