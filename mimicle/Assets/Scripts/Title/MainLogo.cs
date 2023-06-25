using System.Collections;
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
        Text pressT, clickT;
        [SerializeField]
        AudioClip[] presses, clicks;

        AudioSource speaker;
        Animator animator;
        (Color deactive, Color active) colour = (new(0.311f, 0.196f, 0.157f, 1), new(1, 1, 0, 1));
        bool isActivated = false;
        bool isMouseOverOnLogo = false;
        bool timerFlag = true;
        float clickToTransition = 0;
        float panelFadeSpeed = 0.008f;
        float fadingPanelAlpha = 0;
        float logoRotateSpeed = 10;
        float txtColorChangeSpeed = 1;
        float rotationTolerance = 0.1f;
        Stopwatch transitionTimer = new();

        void Start()
        {
            speaker ??= GetComponent<AudioSource>();
            animator = GetComponent<Animator>();
            animator.enabled = false;
            pressT.color = colour.deactive;
            clickT.color = colour.deactive;

            var a = (123).ToString();
            var b = a.Cast<double>();
            print(b.GetType());
        }

        void Update()
        {
            MouseOver();
            Rotation();
        }

        void Rotation()
        {
            if (isMouseOverOnLogo)
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, 0), 10 * Time.deltaTime);
            else
                transform.Rotate(new(0, 0, logoRotateSpeed * Time.deltaTime));
        }

        void MouseOver()
        {
            var cursor = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            var hit = Physics2D.Raycast(cursor, Vector2.up, 1);
            if (hit && hit.collider.gameObject.name == Constant.Logo)
            {
                isMouseOverOnLogo = true;
                transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(2.1f, 2.1f), 20 * Time.deltaTime);
                if (Mynput.Down(0))
                {
                    speaker.PlayOneShot(clicks.ice3());
                    ChangeTextsColor(clickT);
                }
            }
            else if (Mynput.Down(KeyCode.Space))
            {
                speaker.PlayOneShot(presses.ice3());
                ChangeTextsColor(pressT);
            }

            if (!(hit && hit.collider.gameObject.name == Constant.Logo))
            {
                isMouseOverOnLogo = false;
                transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(2, 2, 2), 30 * Time.deltaTime);
            }

            if (isActivated)
            {
                transitionTimer.Start();
                if (transitionTimer.SecondF() >= 1 && timerFlag)
                {
                    StartCoroutine(FadingOutPanel());
                    timerFlag = false;
                }
            }

            if (fadingPanel.color.a >= 1)
                Section.Load(Constant.Main);
        }

        void ChangeTextsColor(Text text)
        {
            text.color = Color.Lerp(colour.deactive, colour.active, txtColorChangeSpeed);
            if (text.color.r >= colour.active.r)
                isActivated = true;
        }

        IEnumerator FadingOutPanel()
        {
            while (fadingPanelAlpha <= 1)
            {
                yield return null;
                fadingPanelAlpha = Mathf.Clamp01(fadingPanelAlpha);
                fadingPanel.color = new(0, 0, 0, fadingPanelAlpha += panelFadeSpeed);
            }
        }
    }
}
