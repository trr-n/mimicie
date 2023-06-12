using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mimical.Extend;

namespace Mimical
{
    public abstract class Enemy : MonoBehaviour
    {
        protected void Start(HP hp) => hp.SetMax();

        protected abstract void Move();

        protected virtual void Left(GameObject gob, int deadLine = -20)
        {
            if (gob.transform.position.x <= deadLine)
            {
                gob.Remove();
            }
        }

        protected void AddSlainCountAndRemove(GameObject gob)
        {
            var slain = GameObject.Find("Wave").GetComponent<Slain>();

            slain.AddCount();

            gob.Remove();
        }
    }
}
