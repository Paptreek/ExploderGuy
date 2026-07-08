using System;
using UnityEngine;
using UnityEngine.InputSystem;
using ExploderGuy.Input;

namespace ExploderGuy.Player
{
    public class PlayerController : MonoBehaviour
    {
        private GameInput _input;
        private Rigidbody2D _rb;
        private Vector2 _moveInput;

        [SerializeField] private float _moveSpeed;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            _input = new GameInput();
        }

        private void OnEnable() => _input.Enable();
        private void OnDisable() => _input.Disable();

        private void Update()
        {
            _moveInput = _input.Player.Move.ReadValue<Vector2>();
            Debug.Log($"Movement Detected: {_moveInput}.");
        }

        private void FixedUpdate()
        {
            Vector2 velocity = _moveInput * _moveSpeed;
            _rb.MovePosition(_rb.position + velocity * Time.fixedDeltaTime);
        }
    }
}
