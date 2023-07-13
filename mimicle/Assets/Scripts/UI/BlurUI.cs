using UnityEngine;
using UnionEngine.Extend;

namespace UnionEngine
{
    public class BlurUI : MonoBehaviour
    {
        [SerializeField]
        Material mat;

        HP hp_player;
        float blur = 0;
        const float MaxBlur = 10f;
        bool max = false;
        public bool Max => max;

        void Start()
        {
            hp_player = GameObject.FindGameObjectWithTag(Constant.Player).GetComponent<HP>();
            mat.SetFloat("_Blur", 0);
        }

        void Update()
        {
            if (hp_player.IsZero)
            {
                blur = Numeric.Clamp(blur, 0, MaxBlur);
                mat.SetFloat("_Blur", blur += Time.unscaledDeltaTime * MaxBlur);
                if (blur >= MaxBlur)
                {
                    max = true;
                }
            }
        }
    }
}