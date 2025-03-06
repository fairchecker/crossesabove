using System.Collections;
using UnityEngine;
using Utils;

namespace View
{
    public class CharacterView : Entity
    {
        private bool _isMoving = false;
        private Animator _animator;
        private SpriteRenderer _spriteRenderer;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        public override void Move(Vector2 movement)
        {
            if (_isMoving) return;
            
            if (movement.x > 0.3)
            {
                StartCoroutine(MovingCooldownCoroutine(new Vector2(0.32f, 0)));
                _animator.SetTrigger("sideRunTrigger");
                _spriteRenderer.flipX = true;
            }
            else if (movement.x < -0.3)
            {
                StartCoroutine(MovingCooldownCoroutine(new Vector2(-0.32f, 0)));
                _animator.SetTrigger("sideRunTrigger");
                _spriteRenderer.flipX = false;
                
            }
            else if (movement.y > 0.3)
            {
                StartCoroutine(MovingCooldownCoroutine(new Vector2(0, 0.32f)));
                _animator.SetTrigger("runTrigger");
                _spriteRenderer.flipX = false;
            }
            else if (movement.y < -0.3)
            {
                StartCoroutine(MovingCooldownCoroutine(new Vector2(0, -0.32f)));
                _animator.SetTrigger("runTrigger");
                _spriteRenderer.flipX = false;
            }
        }

        private IEnumerator MovingCooldownCoroutine(Vector2 direction)
        {
            var targetPosition = new Vector2(transform.position.x + direction.x, transform.position.y  + direction.y);

            if (!PhysicsUtils.CheckPosition(targetPosition) || _isMoving)
            {
                _animator.SetTrigger("idleTrigger");
                yield break;
            }
            
            _isMoving = true;
            while (Vector3.Distance(transform.position, targetPosition) > 0.01f)
            {
                transform.position = Vector2.MoveTowards(transform.position, targetPosition, Speed * Time.deltaTime);
                yield return null;
            }
            
            transform.position = targetPosition;
            _animator.SetTrigger("idleTrigger");
            _isMoving = false;
        }
    }
}