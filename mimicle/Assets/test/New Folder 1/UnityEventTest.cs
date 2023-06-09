using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//UnityEvent���g�����߂ɒǉ����K�v
using UnityEngine.Events;

namespace Sample
{
    public class UnityEventTest : MonoBehaviour
    {
        [SerializeField]
        UnityEvent OnCompleted;

        public void Exec(float wait)
        {
            StartCoroutine(TestCoroutine(wait));
        }

        //��莞�ԑ҂�����Ɏw�肳�ꂽdelegate�̊֐������s����
        IEnumerator TestCoroutine(float wait)
        {
            yield return new WaitForSeconds(wait);

            //UnityEvent�͊֐��̂悤�ɌĂяo���Ȃ��̂�
            // Invoke�Ŏ��s������
            OnCompleted.Invoke();
        }
    }
}
