using UnityEngine;
using UnityEngine.UI;
using Self.Utils;

namespace Self.Game
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
            corectransform.transform.position = boss.transform.position + Offset;
        }

        public void UpdateBossUI()
        {
            if (!boss.IsActive(Active.Self))
            {
                return;
            }

            gauge.fillAmount = bossHp.Ratio;
            text.text = bossHp.Now.ToString();
        }
    }
}
