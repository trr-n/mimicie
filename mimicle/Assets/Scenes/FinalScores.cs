using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Mimical
{
    public class FinalScores : MonoBehaviour
    {
        [SerializeField] Text scores;
        void Update()
        {
            scores.text = $"のこりじかん: {Score.finalTime}\nすこあ: {Score.finalScore}";
        }
    }
}
