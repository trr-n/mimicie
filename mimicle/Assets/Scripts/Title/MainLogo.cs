using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Mimicle.Extend;

namespace Mimicle
{
    public class MainLogo : MonoBehaviour
    {
        [SerializeField]
        Image fadingPanel;
        [SerializeField]
        Text pressT, clickT;
        [SerializeField]
        AudioClip[] presses, clicks;
        [SerializeField]
        Sprite[] logos = new Sprite[2];

        AudioSource speaker;
        (Color inactive, Color active) colours = (
            inactive: new(0.311f, 0.196f, 0.157f, 1),
            active: new(1, 1, 0, 1)
        );
        bool isActivated = false;
        bool isMouseOverOnLogo = false;
        bool timerFlag = true;
        float clickToTransition = 1;
        float fadingPanelAlpha = 0;
        readonly (float PanelFade, float LogoRotate, float TextColorChange) Speeds = (
            PanelFade: 0.008f,
            LogoRotate: 10,
            TextColorChange: 1
        );
        const float rotationTolerance = 0.1f;
        Stopwatch transitionTimer = new();
        Vector3 Scale => new(2, 2, 2);
        bool nowstate = true;
        new SpriteRenderer renderer;

        void Start()
        {
            Time.timeScale = 1;
            speaker = GetComponent<AudioSource>();
            pressT.color = clickT.color = colours.inactive;
            renderer = GetComponent<SpriteRenderer>();
            renderer.sprite = logos[0];
        }

        void Update()
        {
            Rotation();
            MouseOver();
            ChangeSprite();
        }

        void ChangeSprite()
        {
            if (Mynput.Down(KeyCode.Tab))
            {
                if (renderer.Compare(logos[0]))
                {
                    renderer.SetSprite(logos[1]);
                }
                else if (renderer.Compare(logos[1]))
                {
                    renderer.SetSprite(logos[0]);
                }
            }
        }

        void Rotation()
        {
            if (isMouseOverOnLogo)
            {
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, 0), 10 * Time.deltaTime);
                return;
            }
            transform.Rotate(new(0, 0, Speeds.LogoRotate * Time.deltaTime));
        }

        void MouseOver()
        {
            var hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.up, 1);
            if (hit && hit.collider.gameObject.name == Constant.Logo)
            {
                isMouseOverOnLogo = true;
                transform.localScale = Vector3.Lerp(transform.localScale, Scale * 1.1f, 20 * Time.deltaTime);
                if (Mynput.Down(0))
                {
                    speaker.PlayOneShot(clicks.Choice3());
                    ChangeTextsColor(clickT);
                }
            }
            else if (Mynput.Down(KeyCode.Space))
            {
                speaker.PlayOneShot(presses.Choice3());
                ChangeTextsColor(pressT);
            }

            if (!(hit && hit.collider.gameObject.name == Constant.Logo))
            {
                isMouseOverOnLogo = false;
                transform.localScale = Vector3.Lerp(transform.localScale, Scale, 30 * Time.deltaTime);
            }

            if (isActivated)
            {
                transitionTimer.Start();
                if (transitionTimer.SecondF() >= clickToTransition && timerFlag)
                {
                    StartCoroutine(FadingOutPanel());
                    timerFlag = false;
                }
            }

            if (fadingPanel.color.a >= clickToTransition)
            {
                if (renderer.Compare(logos[0]))
                {
                    Site.Load(Constant.Main);
                }
                else if (renderer.Compare(logos[1]))
                {
                    Application.Quit();
                }
            }
        }

        void ChangeTextsColor(Text text)
        {
            text.color = Color.Lerp(colours.inactive, colours.active, Speeds.TextColorChange);
            if (text.color.r >= colours.active.r)
            {
                isActivated = true;
            }
        }

        IEnumerator FadingOutPanel()
        {
            while (fadingPanelAlpha <= 1)
            {
                yield return null;
                fadingPanelAlpha = Mathf.Clamp01(fadingPanelAlpha);
                fadingPanel.color = new(0, 0, 0, fadingPanelAlpha += Speeds.PanelFade);
            }
        }
    }
}
