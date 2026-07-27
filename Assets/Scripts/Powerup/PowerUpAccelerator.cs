using UnityEngine;

namespace ExploderGuy
{
    public class PowerUpAccelerator : MonoBehaviour, IPowerUp
    {
        [field: SerializeField] public bool IsPermanent { get; private set; } = true;
        [field: SerializeField] public int PointValue { get; private set; } = 100;

        public void Apply(PlayerPowerUpReceiver receiver)
        {
            receiver.AddMoveSpeedLevel();
        }
    }
}
