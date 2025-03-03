using UnityEngine;

namespace Interfaces
{
    public interface IMovable
    {
        public float Speed { get; set; }
        public void Move(Vector2 movement);
    }
}