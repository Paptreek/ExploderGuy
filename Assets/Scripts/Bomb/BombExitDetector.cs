using UnityEngine;

namespace ExploderGuy
{
    public class BombExitDetector : MonoBehaviour
    {
        private Bomb _bomb;

        private void Awake()
        {
            _bomb = GetComponentInParent<Bomb>();
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.TryGetComponent<PlayerController>(out _))
            {
                _bomb.RestorePlayerCollision();
            }
        }
    }
}
