using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Mimical.Extend;

namespace Mimical
{
    public class Tests : MonoBehaviour
    {
        [SerializeField]
        Text timeT;

        [SerializeField]
        Text nowWaveT;

        [SerializeField]
        Text maxWaveT;

        [SerializeField]
        Wave wave;

        [SerializeField]
        Image hpImage;

        [SerializeField]
        HP playerHp;

        [SerializeField]
        Text fpsT;

        [SerializeField]
        Image reloadImage;

        [SerializeField]
        Player player;

        [SerializeField]
        Text scoreT;

        [SerializeField]
        Score score;

        [SerializeField]
        WaveData waveData;

        [SerializeField]
        Text waveT;

        Transform playerTransform;

        RectTransform parentTransform;

        void Start()
        {
            playerTransform = GameObject.FindGameObjectWithTag(constant.Player).transform;
            parentTransform = reloadImage.transform.parent.GetComponent<RectTransform>();
        }

        void Update()
        {
            Text();
            Image();
        }

        void Image()
        {
            hpImage.fillAmount = playerHp.Ratio;

            reloadImage.fillAmount = player.ReloadProgress;
            // var playerScreenPos = Camera.main.WorldToScreenPoint(playerTransform.position);
            // RectTransformUtility.ScreenPointToLocalPointInRectangle(parentTransform, playerScreenPos, null, out var uiLocalPos);
            // reloadImage.transform.localPosition = uiLocalPos;
        }

        void Text()
        {
            timeT.text = Score.Time().ToString();
            nowWaveT.text = wave.Now.ToString();
            maxWaveT.text = wave.Max.ToString();
            fpsT.text = visual.Fps().ToString();
            scoreT.text = score.Now.ToString();
            waveT.text = waveData.Now.ToString();
        }
    }
}
