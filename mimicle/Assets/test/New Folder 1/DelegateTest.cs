using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sample
{
    public class DelegateTest : MonoBehaviour
    {
        //�e�X�g�p�ɕ��ʂɐ錾�����֐�
        public void HelloWorld()
        {
            Debug.Log("Hello World!!!");
        }
        public void TodayWeather()
        {
            Debug.Log("�����͉J�ł�");
        }

        //delegate�^�̒�`
        // �֐��̐錾 �u�߂�l�̌^ ���O (�������X�g)�v�O��delegate�Ƃ���
        delegate void PrintMethod();

        // Start is called before the first frame update
        void Start()
        {
            //�ʏ�̊֐��̎��s
            HelloWorld();

            //delegate�^�̕ϐ�
            PrintMethod testMethod = HelloWorld;

            //delegate�^�͂��̂܂܊֐��̂悤�Ɏ��s�ł���
            testMethod();

            //delegate�^�� += �Ŋ֐��̒ǉ����ł���
            testMethod += TodayWeather;
            testMethod += HelloWorld;

            //delegate�^�͎��s���ɕۑ�����Ă��邷�ׂĂ̊֐������ԂɎ��s����
            testMethod();

            //�R���[�`����delegate�^�̕ϐ���n���Ď��s���Ă��炤
            StartCoroutine(TestCoroutine(5, testMethod));

            //�ʃN���X�Ɏ��s���˗�����
            DelegateTestCoroutine other = GetComponent<DelegateTestCoroutine>();
            other.OnCompleted += HelloWorld;
            other.OnCompleted += TodayWeather;
            other.Exec(10);

            //Action�ł̃e�X�g
            ActionFuncTest other2 = GetComponent<ActionFuncTest>();
            other2.OnCompleted += HelloWorld;
            other2.OnCompleted += TodayWeather;
            other2.Exec(10);

            //UnityEvent�ł̃e�X�g
            UnityEventTest other3 = GetComponent<UnityEventTest>();
            //�o�^��UnityEditor��ł����Ȃ��Ă��邽�ߕs�v
            other3.Exec(10);
        }

        //��莞�ԑ҂�����Ɏw�肳�ꂽdelegate�̊֐������s����
        IEnumerator TestCoroutine(float wait, PrintMethod method)
        {
            yield return new WaitForSeconds(wait);

            method();
        }
    }
}
