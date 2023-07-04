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
        // protected abstract void Attack();
        protected virtual void Left(GameObject obj, float deadLine = -10.24f)
        {
            if (obj.transform.position.x <= deadLine)
                Destroy(obj);
        }
        protected void AddSlainCountAndRemove(GameObject gob)
        {
            // GameObject.FindGameObjectWithTag(Constant.WaveManager).GetComponent<Slain>().AddCount();
            Gobject.Find(Constant.WaveManager).GetComponent<Slain>().AddCount();
            Destroy(gob);
        }
        protected void MakeFx(GameObject fx) => fx.Instance();
    }
}
