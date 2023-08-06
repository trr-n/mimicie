using Self.Utils;
using UnityEngine;

namespace Self.Game
{
    public class Scroll : MonoBehaviour
    {
        // [SerializeField]
        GameManager manager;
        [SerializeField]
        Color[] colors;
        [SerializeField]
        GameObject[] backgrounds;

        readonly float speed = 3;
        Vector2 Spawn => new(20f, 0);
        bool scroll = false;

        void Awake()
        {
            if (MyScene.Active() == Constant.Main)
            {
                manager = Gobject.GetWithTag<GameManager>(Constant.Manager);
            }
        }

        void Start()
        {
            foreach (var i in backgrounds)
            {
                i.GetComponent<SpriteRenderer>().color = colors.Choice3();
            }
        }

        void Update()
        {
            if (MyScene.Active() == Constant.Main)
            {
                scroll = manager.Scrollable;
            }

            else
            {
                scroll = true;
            }

            if (!scroll)
            {
                return;
            }

            foreach (var i in backgrounds)
            {
                i.transform.Translate(Time.deltaTime * speed * Vector2.left);

                if (i.transform.position.x <= -20f)
                {
                    i.transform.position = Spawn;
                    i.GetComponent<SpriteRenderer>().color = colors.Choice3();
                }
            }
        }
    }
}
