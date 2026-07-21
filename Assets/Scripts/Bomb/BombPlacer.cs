using UnityEngine;
using System.Collections.Generic;

namespace ExploderGuy
{
    public class BombPlacer : MonoBehaviour
    {
        [SerializeField] private Bomb _bombPrefab;

        private PlayerController _playerController;
        private PlayerState _playerState;
        private Collider2D _playerCollider;
        private List<Bomb> _placedBombs;

        public bool CanPlaceBomb => _placedBombs.Count < _playerState.ActiveBombLimit;

        private void Awake()
        {
            _playerController = GetComponent<PlayerController>();
            _playerCollider = GetComponent<Collider2D>();
            _playerState = GetComponent<PlayerState>();
            _placedBombs = new List<Bomb>();
        }

        private void Update()
        {
            if (_playerController.InteractWasPressedThisFrame)
            {
                TryPlaceBomb();
            }
        }

        public bool TryPlaceBomb()
        {
            if (!CanPlaceBomb)
            {
                return false;
            }

            Bomb bomb = Instantiate(_bombPrefab, transform.position, Quaternion.identity);
            bomb.Initialize(_playerCollider);
            bomb.Exploded += OnBombExploded;
            _placedBombs.Add(bomb);
            return true;
        }

        private void OnBombExploded(Bomb bomb)
        {
            bomb.Exploded -= OnBombExploded;
            _placedBombs.Remove(bomb);
        }
    }
}
