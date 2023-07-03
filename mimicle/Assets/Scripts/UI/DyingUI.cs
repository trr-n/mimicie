using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mimical.Extend;

namespace Mimical
{
    public class DyingUI : MonoBehaviour
    {
        [SerializeField]
        Material mat;

        HP hp_player;
        One one = new();
        float blur = 0f;
        const float MaxBlur = 10f;
        bool blurflag = false;

        void Start()
        {
            hp_player = GameObject.FindGameObjectWithTag(Constant.Player).GetComponent<HP>();
            mat.SetFloat("_Blur", 0);
        }

        void Update()
        {
            if (hp_player.IsZero)
            {
                StartCoroutine(MakeBlur(0.5f));
            }
        }

        IEnumerator MakeBlur(float time)
        {
            var timer = 0f;
            while (timer <= time)
            {
                yield return null;
                timer += time * 2;
                mat.SetFloat("_Blur", blur);
            }
        }
    }
}