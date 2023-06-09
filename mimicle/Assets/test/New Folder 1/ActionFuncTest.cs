using System;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sample
{
    public class ActionFuncTest : MonoBehaviour
    {
        public event Action OnCompleted;

        public void Exec(float wait)
        {
            StartCoroutine(TestCoroutine(wait));
        }

        IEnumerator TestCoroutine(float wait)
        {
            yield return new WaitForSeconds(wait);

            OnCompleted();
        }
    }
}
