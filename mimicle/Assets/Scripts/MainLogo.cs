using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mimical.Extend;

namespace Mimical
{
    public class MainLogo : MonoBehaviour
    {
        [SerializeField]
        Image fadingPanel;

        [SerializeField]
        Text pressT;

        [SerializeField]
        Text clickT;

        [SerializeField]
        AudioClip[] presses;

        [SerializeField]
        AudioClip[] clicks;

        AudioSource speaker;

        Animator animator;

        Color deactive = new(0.311f, 0.196f, 0.157f, 1);

        Color active = new(1, 1, 0, 1);

        bool isActivated = false;

        bool isOver = false;

        bool timerFlag = true;

        float timer = 0;

        float fadingSpeed = 0.008f;

        float panelAlpha = 0;

        float rotateSpeed = 10;

        float speed = 1;

        float torrence = 0.1f;

        void Start()
        {
            speaker ??= GetComponent<AudioSource>();

            animator = GetComponent<Animator>();

            animator.enabled = false;

            pressT.color = deactive;

            clickT.color = deactive;
        }

        void Update()
        {
            MouseOver();

            Rotation();
        }

        void Rotation()
        {
            if (isOver)
            {
                var zDifference = 0 - transform.eulerAngles.z;

                if (zDifference <= torrence && zDifference >= -torrence)
                {
                    "almost 0".show();
                }

                transform.rotation = Quaternion.Lerp(
                    transform.rotation, Quaternion.Euler(0, 0, 0), 10 * Time.deltaTime);
            }

            else
            {
                transform.Rotate(new(0, 0, rotateSpeed * Time.deltaTime));
            }
        }

        void MouseOver()
        {
            var cursor = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            RaycastHit2D hit = Physics2D.Raycast(cursor, Vector2.up, 1);

            if (hit && hit.collider.gameObject.name == constant.Logo)
            {
                isOver = true;

                // animator.enabled = false;

                transform.localScale = Vector3.Lerp(
                    transform.localScale, new Vector3(2.1f, 2.1f), 20 * Time.deltaTime);

                if (input.Down(0))
                {
                    speaker.PlayOneShot(clicks[random.ice(clicks)]);

                    Txt(clickT);
                }
            }

            else if (input.Down(KeyCode.Space))
            {
                speaker.PlayOneShot(presses[random.ice(presses)]);

                Txt(pressT);
            }

            if (!(hit && hit.collider.gameObject.name == constant.Logo))
            {
                isOver = false;

                transform.localScale = Vector3.Lerp(
                    transform.localScale, new Vector3(2, 2, 2), 30 * Time.deltaTime);

                // animator.enabled = true;
            }

            if (isActivated)
            {
                timer += Time.deltaTime;

                if (timer >= 1 && timerFlag)
                {
                    StartCoroutine(FadingOutPanel());

                    timerFlag = false;
                }
            }

            if (fadingPanel.color.a >= 1)
            {
                scene.Load(constant.Main);
            }
        }

        void Txt(Text text)
        {
            text.color = Color.Lerp(deactive, active, speed);

            if (text.color.r >= active.r)
            {
                isActivated = true;
            }
        }

        IEnumerator FadingOutPanel()
        {
            while (panelAlpha <= 1)
            {
                yield return null;

                panelAlpha = panelAlpha.Clamping(0, 1);

                panelAlpha += fadingSpeed;

                fadingPanel.color = new(0, 0, 0, panelAlpha);
            }
        }
    }
}
