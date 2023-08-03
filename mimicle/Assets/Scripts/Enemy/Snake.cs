using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Self.Utils
{
    public class Snake : Enemy
    {
        Transform head;
        Transform[] bodies;
        List<Vector3> positions = new List<Vector3>();

        void Start()
        {
            head = transform;

            int children = transform.childCount;
            bodies = new Transform[children];

            for (int index = 0; index < bodies.Length; index++)
            {
                bodies[index] = transform.GetChild(index);
            }
        }

        void Update()
        {
            Move();
        }

        void GetPosition()
        {
        }

        protected override void Move()
        {
        }
    }
}
