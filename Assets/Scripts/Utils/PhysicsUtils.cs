using UnityEngine;

namespace Utils
{
    public static class PhysicsUtils
    {
        public static bool CheckPosition(Vector2 direction)
        {
            Collider2D hit = Physics2D.OverlapPoint(new Vector2(direction.x, direction.y));
            return hit == null || !hit.CompareTag("Wall");
        }
    }
}