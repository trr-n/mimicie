using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpideTest : MonoBehaviour
{
    [SerializeField]
    GameObject spide;

    float timer = 0f;

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= 1)
        {
            var a = Instantiate(spide, new(0, 0, 1), Quaternion.identity);
            a.GetComponent<UnionEngine.Spide>().SetLevel(2);
            timer = 0f;
        }
    }
}
