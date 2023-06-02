using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mimical.Extend
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
                x is null ? transform.position.x : x.ToSingle(),
                y is null ? transform.position.y : y.ToSingle(),
                z is null ? transform.position.z : z.ToSingle()
            );
        }

        public static void setp(this Transform transform, Vector3 position)
        => transform.position = position;

        public static void setp(this Transform transform, Vector2 position)
        => transform.position = position;

        public static void sestpc22(this Transform transform,
            float? minX = null, float? maxX = null,
            float? minY = null, float? maxY = null,
            float? minZ = null, float? maxZ = null
        )
        {
            if (minX is not null && maxX is not null && minY is not null && maxY is not null && minZ is not null && maxZ is not null)
            {
                throw new NanmoHaittehenwaException();
            }
            transform.position = new Vector3(
                minX is null ? transform.position.x : Mathf.Clamp(transform.position.x, minX.ToSingle(), maxX.ToSingle()),
                minY is null ? transform.position.y : Mathf.Clamp(transform.position.y, minY.ToSingle(), maxY.ToSingle()),
                minZ is null ? transform.position.z : Mathf.Clamp(transform.position.z, minZ.ToSingle(), maxZ.ToSingle())
            );
        }

        public static void setpc2(this Transform transform,
            float? minX = null, float? maxX = null, float? minY = null, float? maxY = null)
        {
            if (minX is null && maxX is null && minY is null && maxY is null)
            {
                throw new NanmoHaittehenwaException();
            }
            transform.position = new(
                Mathf.Clamp(transform.position.x, minX.ToSingle(), maxX.ToSingle()),
                Mathf.Clamp(transform.position.y, minY.ToSingle(), maxY.ToSingle())
            );
        }

        public static void setpc2(this Transform transform, float? x = null, float? y = null)
        {
            if (x is null && y is null)
            {
                throw new NanmoHaittehenwaException();
            }
            transform.position = new(
                Mathf.Clamp(transform.position.x, x.ToSingle(), -x.ToSingle()),
                Mathf.Clamp(transform.position.y, y.ToSingle(), -y.ToSingle())
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
                x is null ? transform.localScale.x : x.ToSingle(),
                y is null ? transform.localScale.y : y.ToSingle(),
                z is null ? transform.localScale.z : z.ToSingle()
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
                x is null ? transform.localScale.x : x.ToSingle(),
                y is null ? transform.localScale.y : y.ToSingle(),
                z is null ? transform.localScale.z : z.ToSingle()
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
