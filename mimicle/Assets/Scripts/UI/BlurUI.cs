using UnityEngine;
using Self.Utils;

namespace Self.Game
{
    public class BlurUI : MonoBehaviour
    {
        [SerializeField]
        Material mat;

        [SerializeField]
        HP boss;

        // HP player;

        const string Blur = "_Blur";

        float blur = 0;
        const float MaxBlur = 30f;

        bool isDone = false;
        public bool IsDone => isDone;

        GameManager manager;

        void Awake()
        {
            mat.SetFloat(Blur, 0);
        }

        void Start()
        {
            manager = Gobject.GetWithTag<GameManager>(Constant.Manager);
        }

        void Update()
        {
            if (boss.IsZero && manager.IsEnd)
            {
                blur = Numeric.Clamp(blur, 0, MaxBlur);
                blur += Time.unscaledDeltaTime * MaxBlur;
                mat.SetFloat(Blur, blur);

                if (blur >= MaxBlur)
                {
                    isDone = true;
                }
            }
        }
    }
}