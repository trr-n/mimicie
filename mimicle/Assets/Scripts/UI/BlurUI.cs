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

        HP player;

        const string Name = "_Blur";

        float blur = 0;
        bool max = false;
        public bool Max => max;
        const float MaxBlur = 10f;

        void Start()
        {
            // player = GameObject.FindGameObjectWithTag(Constant.Player).GetComponent<HP>();
            player = Gobject.GetWithTag<HP>(Constant.Player);
            mat.SetFloat(Name, 0);
        }

        void Update()
        {
            if (player.IsZero || boss.IsZero)
            {
                blur = Numeric.Clamp(blur, 0, MaxBlur);
                // mat.SetFloat(Name, blur += Time.unscaledDeltaTime * MaxBlur);
                blur += Time.unscaledDeltaTime * MaxBlur;
                if (blur >= MaxBlur)
                {
                    max = true;
                }
            }
        }
    }
}