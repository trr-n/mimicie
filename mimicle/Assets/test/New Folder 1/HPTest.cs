using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sample
{
    public class HPTest : MonoBehaviour
    {
        HP test;

        // Start is called before the first frame update
        void Start()
        {
            test = GetComponent<HP>();
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                test.Add(-1);
            }
            if (Input.GetKey(KeyCode.RightArrow))
            {
                test.Add(1);
            }
        }
    }
}
