using UnityEngine;

namespace Mimical.Extend
{
    public static class Coordinate
    {
        public static void setp(this Transform t, double? x = null, double? y = null, double? z = null)
        {
            if (x is null && y is null && z is null)
                throw new Karappoyanke();
            t.position = new(x is null ? t.position.x : x.Single(), y is null ? t.position.y : y.Single(), z is null ? t.position.z : z.Single());
        }
        public static void setp(this Transform transform, Vector3 position) => transform.position = position;
        public static void setp(this Transform transform, Vector2 position) => transform.position = position;
        public static void setpc22(this Transform t, float? minX = null, float? maxX = null, float? minY = null, float? maxY = null, float? minZ = null, float? maxZ = null)
        {
            if (minX is not null && maxX is not null && minY is not null && maxY is not null && minZ is not null && maxZ is not null)
                throw new Karappoyanke();
            t.position = new(minX is null ? t.position.x : Mathf.Clamp(t.position.x, minX.Single(), maxX.Single()),
                minY is null ? t.position.y : Mathf.Clamp(t.position.y, minY.Single(), maxY.Single()),
                minZ is null ? t.position.z : Mathf.Clamp(t.position.z, minZ.Single(), maxZ.Single()));
        }
        public static void setpc2(this Transform t, float? minX = null, float? maxX = null, float? minY = null, float? maxY = null)
        {
            if (minX is null && maxX is null && minY is null && maxY is null)
                throw new Karappoyanke();
            t.position = new(Mathf.Clamp(t.position.x, minX.Single(), maxX.Single()), Mathf.Clamp(t.position.y, minY.Single(), maxY.Single()));
        }
        public static void setpc2(this Transform t, float? x = null, float? y = null)
        {
            if (x is null && y is null)
                throw new Karappoyanke();
            t.position = new(Mathf.Clamp(t.position.x, x.Single(), -x.Single()), Mathf.Clamp(t.position.y, y.Single(), -y.Single()));
        }
        public static void setr(this Transform t, double? x = null, double? y = null, double? z = null)
        {
            if (x is null && y is null && z is null)
                throw new Karappoyanke();
            t.rotation = Quaternion.Euler(x is null ? t.localScale.x : x.Single(), y is null ? t.localScale.y : y.Single(), z is null ? t.localScale.z : z.Single());
        }
        public static void setr(this Transform t, Quaternion rotation) => t.rotation = rotation;
        public static void sets(this Transform t, double? x = null, double? y = null, double? z = null)
        {
            if (x is null && y is null && z is null)
                throw new Karappoyanke();
            t.localScale = new(x is null ? t.localScale.x : x.Single(), y is null ? t.localScale.y : y.Single(), z is null ? t.localScale.z : z.Single());
        }
        public static void sets(this Transform t, Vector3 scale) => t.localScale = scale;
        public static void move2d(this Transform t, float speed, string axish = "Horizontal", string axisv = "Vertical")
        => t.Translate(new Vector2(Input.GetAxis(axish), Input.GetAxis(axisv)) * speed * Time.deltaTime);
        public static bool Twins(Vector3 n1, Vector3 n2) => Mathf.Approximately(n1.x, n2.x) && Mathf.Approximately(n1.y, n2.y) && Mathf.Approximately(n1.z, n2.z);
    }
}
