using UnityEngine;
using ExploderGuy.Player;

namespace ExploderGuy.Bomb
{
    public class BombPlacer : MonoBehaviour
    {
        [SerializeField] private Bomb _bombPrefab;

        private PlayerController _playerController;
        private Collider2D _playerCollider;

        private void Awake()
        {
            _playerController = GetComponent<PlayerController>();
            _playerCollider = GetComponent<Collider2D>();
        }

        private void Update()
        {
            if (_playerController.InteractWasPressedThisFrame)
            {
                PlaceBomb();
            }
        }

        public void PlaceBomb()
        {
            Bomb bomb = Instantiate(_bombPrefab, transform.position, Quaternion.identity);
            bomb.Initialize(_playerCollider);
        }
    }
}
