using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sample
{
    public class AA : MonoBehaviour
    {
        [SerializeField]
        float[] ds;

        void Update()
        {
            //? for --------------------------------------------------------------------
            var enemies = GameObject.FindGameObjectsWithTag("Enemy");

            float minDistance = 0;
            int findIndex = -1;

            for (int i = 0; i < enemies.Length; i++)
            {
                var hp = enemies[i].GetComponent<HP>();
                if (hp.Current <= 0)
                {
                    continue;
                }

                float distance = Vector3.Distance(transform.position, enemies[i].transform.position);

                if (findIndex == -1 || distance <= minDistance)
                {
                    minDistance = distance;
                    findIndex = i;
                }
            }

            if (findIndex >= 0)
            {
                Debug.Log(enemies[findIndex].name);
            }

            //? with Linq --------------------------------------------------------------
            //? 配列に対して一致するものを探して抽出できる
            var nearEnemy = enemies
                // 指定した条件に一致するものだけ抽出
                .Where(enemy => enemy.GetComponent<HP>().Current > 0)
                // 指定した値をキーに昇順で並べ替える
                .OrderBy(enemy => Vector3.Distance(transform.position, enemy.transform.position))
                // 先頭の要素を取り出す、なければnull
                .FirstOrDefault();

            //! 一致するものだけ抽出　→　並べ替え → 先頭を取り出す
            // linq 拡張メソッド一覧
            //* https://qiita.com/Apeworks/items/3575bcb90b6097dd6740
            //! わかりやすく記述できるが、パフォーマンス面ではfor文生牡蠣に劣る

            // var enHps = enemies.Select(en => en.GetComponent<HP>()).ToArray();

            // var hoge = enemies.Select(en => new { hp = en.GetComponent<HP>(), transform = en.transform }).ToArray();
            // var hogeT = hoge[0].transform.position;
            // var hogeHp = hoge[0].hp;

            if (nearEnemy is not null)
            {
                Debug.Log($"Linq: {nearEnemy.name}");
            }

            //? with Linq クエリ式 ------------------------------------------------------
            //! https://learn.microsoft.com/ja-jp/dotnet/csharp/linq/query-expression-basics
            //? 内部は普通のLinqと同じ
            var nearestEnemy = (
                from enemy in enemies
                where enemy.GetComponent<HP>().Current < 0
                orderby Vector3.Distance(transform.position, enemy.transform.position)
                select enemy)
                .FirstOrDefault();

            if (nearestEnemy != null)
            {
                Debug.Log($"Query: {nearestEnemy}");
            }
        }

        IEnumerator AAA()
        {
            while (true)
            {
                yield return new WaitUntil(() => Input.anyKeyDown);

                Debug.Log("AAA");
            }
        }
    }
}
