using UnityEngine;
using System.Collections.Generic;

namespace ExploderGuy
{
    public class PlayerBombPlacer : MonoBehaviour
    {
        [SerializeField] private Bomb _bombPrefab;
        [SerializeField] private int _startingBombLimit;
        [SerializeField] private int _startingBlastRadius;

        private PlayerController _playerController;
        private Collider2D _playerCollider;
        private List<Bomb> _placedBombs;
        private PlayerBombAttributes _bombAttributes;

        public bool CanPlaceBomb => _placedBombs.Count < _bombAttributes.ActiveBombLimit;

        private void Awake()
        {
            _bombAttributes = new PlayerBombAttributes(_startingBombLimit, _startingBlastRadius);

            _playerController = GetComponent<PlayerController>();
            _playerCollider = GetComponent<Collider2D>();
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
            bomb.Initialize(new BombPlacementContext(_playerCollider, _bombAttributes.BlastRadius));
            bomb.Exploded += OnBombExploded;
            _placedBombs.Add(bomb);
            return true;
        }

        private void OnBombExploded(Bomb bomb)
        {
            bomb.Exploded -= OnBombExploded;
            _placedBombs.Remove(bomb);
        }

        public void IncreaseBombLimit() => _bombAttributes.IncreaseBombLimit();
        public void IncreaseBlastRadius() => _bombAttributes.IncreaseBlastRadius();
        public void UnlockKick() => _bombAttributes.UnlockKick();
        public void UnlockRemoteDetonator() => _bombAttributes.UnlockRemoteDetonator();
    }
}
