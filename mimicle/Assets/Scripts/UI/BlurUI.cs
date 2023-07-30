using UnityEngine;
using Self.Utility;

namespace Self
{
    public class BlurUI : MonoBehaviour
    {
        [SerializeField]
        Material mat;

        [SerializeField]
        HP boss;

        HP player;

        float blur = 0;
        bool max = false;
        public bool Max => max;
        const float MaxBlur = 10f;

        void Start()
        {
            player = GameObject.FindGameObjectWithTag(Constant.Player).GetComponent<HP>();
            mat.SetFloat("_Blur", 0);
        }

        void Update()
        {
            if (player.IsZero || boss.IsZero)
            {
                blur = Numeric.Clamp(blur, 0, MaxBlur);
                // mat.SetFloat("_Blur", blur += Time.unscaledDeltaTime * MaxBlur);
                blur += Time.unscaledDeltaTime * MaxBlur;
                if (blur >= MaxBlur)
                {
                    max = true;
                }
            }
        }

        public void Reblur()
        {
            // mat.SetFloat("_Blur", 0);
            // mat.SetFloat("_Blur", MaxBlur);
        }
    }
}