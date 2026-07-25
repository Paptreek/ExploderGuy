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

        public void AddMoveSpeedLevel()
        {
            _playerState.AddMoveSpeedLevel();
        }

        public void IncreaseBombLimit()
        {
            _bombPlacer.IncreaseBombLimit();
        }
    }
}
