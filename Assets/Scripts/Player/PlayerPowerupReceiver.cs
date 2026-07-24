namespace ExploderGuy
{
    public class PlayerPowerUpReceiver
    {
        private PlayerBombPlacer _bombPlacer;
        private PlayerState _playerState;

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
