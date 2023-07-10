using UnityEngine;

namespace Mimical.Extend
{
    public static class Coordinate
    {
        public static void SetPosition(this Transform transform, double? x = null, double? y = null, double? z = null)
        {
            if (x is null && y is null && z is null)
            {
                throw new Karappoyanke();
            }
            transform.position = new(x is null ? transform.position.x : x.Single(), y is null ? transform.position.y : y.Single(), z is null ? transform.position.z : z.Single());
        }
        public static void SetPosition(this Transform transform, Vector3 position) => transform.position = position;
        public static void SetPosition(this Transform transform, Vector2 position) => transform.position = position;
        public static void ClampPosition22(this Transform t, float? minX = null, float? maxX = null, float? minY = null, float? maxY = null, float? minZ = null, float? maxZ = null)
        {
            if (minX is not null && maxX is not null && minY is not null && maxY is not null && minZ is not null && maxZ is not null)
            {
                throw new Karappoyanke();
            }
            t.position = new(minX is null ? t.position.x : Mathf.Clamp(t.position.x, minX.Single(), maxX.Single()),
                minY is null ? t.position.y : Mathf.Clamp(t.position.y, minY.Single(), maxY.Single()),
                minZ is null ? t.position.z : Mathf.Clamp(t.position.z, minZ.Single(), maxZ.Single()));
        }
        public static void ClampPosition2(this Transform t, float? minX = null, float? maxX = null, float? minY = null, float? maxY = null)
        {
            if (minX is null && maxX is null && minY is null && maxY is null)
            {
                throw new Karappoyanke();
            }
            t.position = new(Mathf.Clamp(t.position.x, minX.Single(), maxX.Single()), Mathf.Clamp(t.position.y, minY.Single(), maxY.Single()));
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
            t.position = new(Mathf.Clamp(t.position.x, x.Single(), -x.Single()), Mathf.Clamp(t.position.y, y.Single(), -y.Single()));
        }
        public static void SetRotation(this Transform t, double? x = null, double? y = null, double? z = null)
        {
            if (x is null && y is null && z is null)
            {
                throw new Karappoyanke();
            }
            t.rotation = Quaternion.Euler(x is null ? t.localScale.x : x.Single(), y is null ? t.localScale.y : y.Single(), z is null ? t.localScale.z : z.Single());
        }
        public static void SetRotation(this Transform t, Quaternion rotation) => t.rotation = rotation;
        public static void SetScale(this Transform t, double? x = null, double? y = null, double? z = null)
        {
            if (x is null && y is null && z is null)
            {
                throw new Karappoyanke();
            }
            t.localScale = new(x is null ? t.localScale.x : x.Single(), y is null ? t.localScale.y : y.Single(), z is null ? t.localScale.z : z.Single());
        }
        public static void SetScale(this Transform t, Vector3 scale) => t.localScale = scale;
        public static void Move2(this Transform t, float speed, string axish = "Horizontal", string axisv = "Vertical")
        => t.Translate(new Vector2(Input.GetAxis(axish), Input.GetAxis(axisv)) * speed * Time.deltaTime);
        public static bool Twins(Vector3 n1, Vector3 n2) => Mathf.Approximately(n1.x, n2.x) && Mathf.Approximately(n1.y, n2.y) && Mathf.Approximately(n1.z, n2.z);
    }
}
