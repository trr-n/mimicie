using UnityEngine;

namespace Mimicle.Extend
{
    public static class Coordinate
    {
        public static void SetPosition(this Transform t, double? x = null, double? y = null, double? z = null)
        {
            if (x is null && y is null && z is null)
            {
                throw new Karappoyanke();
            }
            t.position = new(x is null ? t.position.x : (float)x, y is null ? t.position.y : (float)y, z is null ? t.position.z : (float)z);
        }
        public static void SetPosition(this Transform t, Vector3 position) => t.position = position;
        public static void SetPosition(this Transform t, Vector2 position) => t.position = position;
        public static void ClampPosition22(this Transform t, float? minX = null, float? maxX = null, float? minY = null, float? maxY = null, float? minZ = null, float? maxZ = null)
        {
            if (minX is not null && maxX is not null && minY is not null && maxY is not null && minZ is not null && maxZ is not null)
            {
                throw new Karappoyanke();
            }
            t.position = new(minX is null ? t.position.x : Mathf.Clamp(t.position.x, (float)minX, (float)maxX),
                minY is null ? t.position.y : Mathf.Clamp(t.position.y, (float)minY, (float)maxY),
                minZ is null ? t.position.z : Mathf.Clamp(t.position.z, (float)minZ, (float)maxZ));
        }
        public static void ClampPosition2(this Transform t, float? minX = null, float? maxX = null, float? minY = null, float? maxY = null)
        {
            if (minX is null && maxX is null && minY is null && maxY is null)
            {
                throw new Karappoyanke();
            }
            t.position = new(Mathf.Clamp(t.position.x, (float)minX, (float)maxX), Mathf.Clamp(t.position.y, (float)minY, (float)maxY));
        }
        public static void ClampPosition2(this Transform t, (float min, float max)? x = null, (float min, float max)? y = null)
        {
            if (x is null && y is null)
            {
                throw new Karappoyanke();
            }
            t.position = new(Mathf.Clamp(t.position.x, x.Value.max, x.Value.max), Mathf.Clamp(t.position.y, y.Value.min, y.Value.max));
        }
        public static void ClampPosition2(this Transform t, float? x = null, float? y = null)
        {
            if (x is null && y is null)
            {
                throw new Karappoyanke();
            }
            t.position = new(Mathf.Clamp(t.position.x, (float)x, (float)-x), Mathf.Clamp(t.position.y, (float)y, (float)-y));
        }
        public static void SetRotation(this Transform t, double? x = null, double? y = null, double? z = null)
        {
            if (x is null && y is null && z is null)
            {
                throw new Karappoyanke();
            }
            t.rotation = Quaternion.Euler(x is null ? t.localScale.x : (float)x, y is null ? t.localScale.y : (float)y, z is null ? t.localScale.z : (float)z);
        }
        public static void SetRotation(this Transform t, Quaternion rotation) => t.rotation = rotation;
        public static void SetScale(this Transform t, double? x = null, double? y = null, double? z = null)
        {
            if (x is null && y is null && z is null)
            {
                throw new Karappoyanke();
            }
            t.localScale = new(x is null ? t.localScale.x : (float)x, y is null ? t.localScale.y : (float)y, z is null ? t.localScale.z : (float)z);
        }
        public static void SetScale(this Transform t, Vector3 scale) => t.localScale = scale;
        public static void Move2(this Transform t, float speed, string axish = "Horizontal", string axisv = "Vertical")
        => t.Translate(new Vector2(Input.GetAxis(axish), Input.GetAxis(axisv)) * speed * Time.deltaTime);
        public static bool Twins(Vector3 n1, Vector3 n2) => Mathf.Approximately(n1.x, n2.x) && Mathf.Approximately(n1.y, n2.y) && Mathf.Approximately(n1.z, n2.z);
    }
}
