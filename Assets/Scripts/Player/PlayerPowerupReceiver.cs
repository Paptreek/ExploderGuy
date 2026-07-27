using UnityEngine;

namespace ExploderGuy
{
    public class PlayerPowerUpReceiver : MonoBehaviour
    {
        private PlayerBombPlacer _bombPlacer;
        private PlayerState _playerState;

        private void Awake()
        {
            _bombPlacer = GetComponent<PlayerBombPlacer>();
            _playerState = GetComponent<PlayerState>();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!collision.TryGetComponent(out IPowerUp powerUp))
            {
                return;
            }

            powerUp.Apply(this);
            Destroy(collision.gameObject);
        }

        public void AddMoveSpeedLevel() => _playerState.AddMoveSpeedLevel();
        public void IncreaseBombLimit() => _bombPlacer.IncreaseBombLimit();
        public void IncreaseBlastRadius() => _bombPlacer.IncreaseBlastRadius();
    }
}
