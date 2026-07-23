using UnityEngine;

namespace ExploderGuy
{
    public class Accelerator : MonoBehaviour
    {
        [field: SerializeField] public bool IsPermanent { get; private set; } = true;
        [field: SerializeField] public int PointValue { get; private set; } = 100;

        public void Apply(PlayerState playerState)
        {
            playerState.AddMoveSpeedLevel();
        }
    }
}
