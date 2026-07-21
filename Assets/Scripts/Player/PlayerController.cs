using UnityEngine;
using ExploderGuy.Input;

namespace ExploderGuy
{
    public class PlayerController : MonoBehaviour
    {

        private GameInput _input;
        private Rigidbody2D _rb;
        private Vector2 _moveInput;
        private PlayerState _playerState;
        public bool InteractWasPressedThisFrame => _input.Player.Interact.WasPressedThisFrame();

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            _input = new GameInput();
            _playerState = GetComponent<PlayerState>();
        }

        private void OnEnable() => _input.Enable();
        private void OnDisable() => _input.Disable();

        private void Update()
        {
            _moveInput = _input.Player.Move.ReadValue<Vector2>();
        }

        private void FixedUpdate()
        {
            Vector2 velocity = _moveInput * _playerState.MoveSpeed;
            _rb.MovePosition(_rb.position + velocity * Time.fixedDeltaTime);
        }
    }
}
