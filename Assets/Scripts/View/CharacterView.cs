using System.Collections;
using Interfaces;
using UnityEngine;

namespace View
{
    public class CharacterView : MonoBehaviour, IMovable
    {
        public float Speed { get; set; }
        private Rigidbody2D _rigidbody2D;
        private bool _isMoving;

        public void Move(Vector2 movement)
        {
            if (_isMoving) return;
            
            if (movement.x > 0.3)
            {
                StartCoroutine(MovingCooldownCoroutine(new Vector2(0.32f, 0)));
            }
            else if (movement.x < -0.3)
            {
                StartCoroutine(MovingCooldownCoroutine(new Vector2(-0.32f, 0)));
            }
            else if (movement.y > 0.3)
            {
                StartCoroutine(MovingCooldownCoroutine(new Vector2(0, 0.32f)));
            }
            else if (movement.y < -0.3)
            {
                StartCoroutine(MovingCooldownCoroutine(new Vector2(0, -0.32f)));
            }
        }
        


        private IEnumerator MovingCooldownCoroutine(Vector2 direction)
        {
            Vector2 targetPosition = new Vector2(transform.position.x + direction.x, transform.position.y + direction.y);
            _isMoving = true;
            while (Vector3.Distance(transform.position, targetPosition) > 0.01f)
            {
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, Speed * Time.deltaTime);
                yield return null;
            }
            
            transform.position = targetPosition;
            _isMoving = false;
        }

        public void Initialize()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }
    }
}