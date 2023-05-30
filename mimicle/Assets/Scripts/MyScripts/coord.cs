using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mimic.Extend
{
    public static class coord
    {
        public static void setp(this Transform transform,
            double? x = null, double? y = null, double? z = null)
        {
            // 全部空で なんもはいってへんわエクセプション 発動
            if (x is null && y is null && z is null)
            {
                throw new NanmoHaittehenwaException();
            }
            transform.position = new(
                x is null ? transform.position.x : x.single(),
                y is null ? transform.position.y : y.single(),
                z is null ? transform.position.z : z.single()
            );
        }

        public static void setp(this Transform transform, Vector3 position)
        => transform.position = position;

        public static void setpc2(this Transform transform,
            float? minX = null, float? minY = null,
            float? maxX = null, float? maxY = null
        )
        {
            transform.position = new(
                x: Mathf.Clamp(transform.position.x, minX.single(), minX.single()),
                y: Mathf.Clamp(transform.position.y, minY.single(), minY.single())
            );
        }

        // public static void setr(this Transform transform,
        //     double? eulerX = null, double? eulerY = null, double? eulerZ = null)
        // {
        //     // 全部空で なんもはいってへんわエクセプション 発動
        //     if (eulerX is null && eulerY is null && eulerZ is null)
        //     {
        //         throw new NanmoHaittehenwaException();
        //     }

        //     transform.rotation = Quaternion.Euler(
        //         eulerX is not null ? eulerX.single() : transform.eulerAngles.x,
        //         eulerY is not null ? eulerY.single() : transform.eulerAngles.y,
        //         eulerZ is not null ? eulerZ.single() : transform.eulerAngles.z
        //     );
        // }

        public static void setr(this Transform transform,
            double? x = null, double? y = null, double? z = null)
        {
            if (x is null && y is null && z is null)
            {
                throw new NanmoHaittehenwaException();
            }
            transform.rotation = Quaternion.Euler(
                x is null ? transform.localScale.x : x.single(),
                y is null ? transform.localScale.y : y.single(),
                z is null ? transform.localScale.z : z.single()
            );
        }

        public static void setr(this Transform transform, Quaternion rotation)
        => transform.rotation = rotation;

        public static void sets(this Transform transform,
            double? x = null, double? y = null, double? z = null)
        {
            if (x is null && y is null && z is null)
            {
                throw new NanmoHaittehenwaException();
            }
            transform.localScale = new Vector3(
                x is null ? transform.localScale.x : x.single(),
                y is null ? transform.localScale.y : y.single(),
                z is null ? transform.localScale.z : z.single()
            );
        }

        public static void sets(this Transform transform, Vector3 scale)
            => transform.localScale = scale;

        public static void move2d(this Transform transform,
                float speed, bool multiplyTime = true, string axish = "Horizontal", string axisv = "Vertical")
        {
            float h = Input.GetAxis(axish), v = Input.GetAxis(axisv);
            Vector3 move = new(h, v);
            Vector3 move2 = move * speed;
            transform.Translate(multiplyTime ? move2 * Time.deltaTime : move2);
        }
    }
}
