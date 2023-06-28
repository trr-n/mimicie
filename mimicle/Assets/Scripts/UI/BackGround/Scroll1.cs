using UnityEngine;

namespace Mimical
{
    public class Scroll1 : MonoBehaviour
    {
        [SerializeField]
        GameObject[] cores;
        [SerializeField]
        float scrollSpeed = 1;

        void Update()
        {
            foreach (var i in cores)
            {
                i.transform.Translate(Vector2.left * scrollSpeed * Time.deltaTime);
                if (i.transform.position.x <= -Scroll.Left)
                    i.transform.position = new(Scroll.Left, 0);
            }
        }
    }
}