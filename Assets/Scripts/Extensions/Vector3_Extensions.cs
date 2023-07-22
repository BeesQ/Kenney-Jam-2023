using UnityEngine;

namespace Extensions
{
    public static class Vector3_Extensions
    {
        public static Vector3 GetMouseDirectionVector(this Vector3 position)
        {
            Vector3 mousePosition = Input.mousePosition;
            mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
            mousePosition.z = 0f;

            Vector3 dir = mousePosition - position;
            dir.Normalize();

            return dir;
        }
    }
}