using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mimical.Extend;

namespace Mimical
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField]
        GameObject test;

        [SerializeField]
        float posDistance;

        [Tooltip("移動距離")]
        float speed = 2f; // = 0.5f;

        new Rigidbody2D rigidbody;

        Vector2 preCursorPos;
        Vector2 movingDirection;
        Vector2 currentCursorPos;
        Vector2 selfp;
        public Vector2 SelfPos => selfp;

        readonly float[] Clamps = new float[3] { 2.5f, -4.75f, 4.25f };

        bool isCtrl = true;

        void Start()
        {
            rigidbody = GetComponent<Rigidbody2D>();

            Physics2D.gravity = new Vector3(0, 0, -9.81f);
        }

        void Update()
        {
            selfp = transform.position;
            ClampingPos();

            ChangeControl();
            Move0(redRatio: 0.5f);
            Move2();
            // GetMovingCursorDirection(clickPosDistance: posDistance);
        }

        void ClampingPos()
        {
            var position = transform.position;
            position.x = Mathf.Clamp(transform.position.x, -Clamps[0], Clamps[0]);
            position.y = Mathf.Clamp(transform.position.y, -Clamps[1], Clamps[2]);
            transform.position = position;
        }

        void ChangeControl()
        {
            isCtrl.show();
            if (input.Down(KeyCode.Space))
            {
                if (!isCtrl)
                {
                    isCtrl = true;
                }
                else if (isCtrl)
                {
                    isCtrl = false;
                }
            }
        }

        void Move0(float redRatio)
        {
            if (isCtrl)
            {
                return;
            }
            float h = Input.GetAxis(cst.Horizontal), v = Input.GetAxis(cst.Vertical);
            Vector2 hv = h * Vector2.right + v * Vector2.up;
            // hv.magnitude.show();
            if (hv.magnitude > 1)
            {
                speed *= redRatio;
                return;
            }
            rigidbody.velocity += hv * speed * Time.deltaTime;
        }

        Vector2 pre;
        void Move2()
        {
            pre = Input.mousePosition;
            if (!isCtrl)
            {
                return;
            }
            var cursor = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            cursor.z = 0;
            this.transform.position = cursor;
        }

        void GetMovingCursorDirection(int click = 0, float clickPosDistance = 1f)
        {
            preCursorPos = Input.mousePosition;
            if (input.Down(click))
            {
                currentCursorPos = Input.mousePosition;
                "down".show();
                // ("down prepos: " + preCursorPos + ", cupos: " + currentCursorPos).show();
            }

            if (input.Up(click))
            {
                if (Vector2.Distance(preCursorPos, currentCursorPos) < clickPosDistance)
                {
                    return;
                }
                movingDirection = preCursorPos - currentCursorPos;
                "up".show();
                // ("up prepos: " + preCursorPos + ", cupos: " + currentCursorPos).show();
            }
            // rigidbody.velocity += direction * speed * Time.deltaTime;
            if (movingDirection == Vector2.zero)
            {
                return;
            }
            movingDirection.show();
            transform.Translate(movingDirection * 0.5f);
            movingDirection = Vector2.zero;
        }

        void testf()
        {
            var t = test.ins(transform.position, Quaternion.identity);
            t.GetComponent<Rigidbody2D>().velocity = transform.up * 100 * Time.deltaTime;

            Destroy(t, 10);
        }

        void Move1()
        {
            if (input.Down(KeyCode.A))
            {
                transform.Translate(Vector2.left * speed);
            }

            else if (input.Down(KeyCode.D))
            {
                transform.Translate(Vector2.right * speed);
            }

            else if (input.Down(KeyCode.W))
            {
                transform.Translate(Vector2.up * speed);
            }

            else if (input.Down(KeyCode.S))
            {
                transform.Translate(Vector2.down * speed);
            }
        }
    }
}
