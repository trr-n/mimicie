using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sample
{
    public class DelegateTestCoroutine : MonoBehaviour
    {
        //delegate�^�̒�`
        // �֐��̐錾 �u�߂�l�̌^ ���O (�������X�g)�v�O��delegate�Ƃ���
        public delegate void PrintMethod();

        //�R���[�`���������Ɏ��s���Ăق��������̓o�^
        // event ������ƊO������͒ǉ��A�폜�݂̂��\�ɂȂ�
        // ���s�͓����̂�
        public event PrintMethod OnCompleted;

        public void Exec(float wait)
        {
            StartCoroutine(TestCoroutine(wait));
        }

        //��莞�ԑ҂�����Ɏw�肳�ꂽdelegate�̊֐������s����
        IEnumerator TestCoroutine(float wait)
        {
            yield return new WaitForSeconds(wait);

            OnCompleted();
        }
    }
}
