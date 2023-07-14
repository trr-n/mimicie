using UnityEngine;
using UnityEngine.UI;
using Feather.Utils;

namespace Feather
{
    public class BossUI : MonoBehaviour
    {
        [SerializeField]
        GameObject core, boss;

        [SerializeField]
        Image gauge;

        [SerializeField]
        Text text;

        [SerializeField]
        HP bossHp;

        RectTransform corectransform;
        Vector3 Offset => new(0, 2);

        Color gaugeColor;
        float hue = 100f;

        void Start()
        {
            Canvas.ForceUpdateCanvases();
            corectransform = core.GetComponent<RectTransform>();
        }

        void Update()
        {
            Position();
        }

        void Position()
        {
            if (!boss.IsActive(Active.Self))
            {
                return;
            }
            var bossPos = boss.transform.position;
            corectransform.transform.position = bossPos + Offset;
        }

        public void UpdateBossUI()
        {
            if (!boss.IsActive(Active.Self))
            {
                return;
            }
            // UpdateGaugeColor();
            gauge.fillAmount = bossHp.Ratio;
            text.text = bossHp.Now.ToString();
        }

        // public void UpdateGaugeColor()
        // {
        //     // 100 - 0
        //     gaugeColor = Color.HSVToRGB(gauge.fillAmount * Mathf.Deg2Rad / 360, 1, 1);
        //     gauge.color = gaugeColor;
        // }
    }
}
