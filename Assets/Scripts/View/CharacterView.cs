using System.Collections;
using Interfaces;
using UnityEngine;

namespace View
{
    public class CharacterView : MonoBehaviour, IMovable
    {
        public float Speed { get; set; }
        private Rigidbody2D _rigidbody2D;
        private bool _isMoving = false;

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

        private bool CheckPosition(Vector2 direction)
        {
            Collider2D hit = Physics2D.OverlapPoint(new Vector2(direction.x, direction.y));
            return hit == null || !hit.CompareTag("Wall");
        }

        private IEnumerator MovingCooldownCoroutine(Vector2 direction)
        {
            Vector2 targetPosition = new Vector2(transform.position.x + direction.x, transform.position.y  + direction.y);
            Debug.Log(targetPosition.x + ", " + targetPosition.y);
            if (!CheckPosition(targetPosition) || _isMoving) yield break;
            _isMoving = true;
            Debug.Log("Ok");
            while (Vector3.Distance(transform.position, targetPosition) > 0.01f)
            {
                transform.position = Vector2.MoveTowards(transform.position, targetPosition, Speed * Time.deltaTime);
                yield return null;
            }
            
            transform.position = targetPosition;
            _isMoving = false;
        }

        public void Initialize(float speed)
        {
            Speed = speed;
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }
    }
}