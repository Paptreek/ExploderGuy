using UnityEngine;

namespace ExploderGuy
{
    public class PlayerPickup : MonoBehaviour
    {
        private IPowerUp _powerUp;

        private void Awake()
        {
            _powerUp = GetComponent<IPowerUp>();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            bool isPlayer = other.TryGetComponent(out PlayerState playerState);

            if (!isPlayer)
            {
                return;
            }

            _powerUp.Apply(playerState);
            Destroy(gameObject);
        }
    }
}
