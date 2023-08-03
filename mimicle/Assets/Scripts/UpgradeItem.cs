using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Self.Utils;

namespace Self
{
    public class UpgradeItem : MonoBehaviour
    {
        [SerializeField]
        GameObject removeEffect;

        float[] Lives => new float[] { 5f, 10f };
        float lifeTime = 5f;

        void Start()
        {
            print(lifeTime = Mathf.Clamp(lifeTime, Lives[0], Lives[1]));
            Runtime.Book(lifeTime, () => removeEffect.Generate(transform.position));
            Destroy(gameObject, lifeTime);
        }
    }
}
