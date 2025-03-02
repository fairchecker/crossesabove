using Interfaces;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Controller
{
    public class ActorController : MonoBehaviour
    {
        private InputSystem_Actions _inputSystem;
        private IMovable _actor;

        private void Awake()
        {
            _inputSystem = new InputSystem_Actions();
            _inputSystem.Enable();
            _actor = GetComponent<IMovable>();
        }

        private void FixedUpdate()
        {
            ReadMovement();
        }

        private void ReadMovement()
        {
            var direction = _inputSystem.Player.Move.ReadValue<Vector2>();
            _actor.Move(direction);
        }
    }
}

