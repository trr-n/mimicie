using UnityEngine;
using UnityEngine.UI;

namespace Mimical
{
    public class _HPColor : MonoBehaviour
    {
        [SerializeField]
        Image hpBar;
        [SerializeField]
        HP playerHp;

        void Update() => hpBar.fillAmount = playerHp.Ratio;
    }
}
