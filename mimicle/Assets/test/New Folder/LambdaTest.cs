using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sample
{
    public class LambdaTest : MonoBehaviour
    {
        void Start()
        {
            // (引数リスト) => 処理
            StartCoroutine(TestCoroutine(3f, () => Debug.Log("Lambda test")));

            // 引数の型から推論してくれる
            StartCoroutine(TestCoroutine2(4f, (x) => Debug.Log("Lambda test" + x)));
        }

        IEnumerator TestCoroutine(float wait, Action OnCompleted)
        {
            yield return new WaitForSeconds(wait);

            OnCompleted();
        }

        IEnumerator TestCoroutine2(float wait, Action<int> OnCompleted)
        {
            int errorCode = 0;

            yield return new WaitForSeconds(wait);

            OnCompleted(errorCode);
        }
    }
}
