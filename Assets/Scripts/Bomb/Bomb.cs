using UnityEngine;

namespace ExploderGuy.Bomb
{
    public class Bomb : MonoBehaviour
    {
        private Collider2D _bombCollider;
        private Collider2D _playerCollider;

        private void Awake()
        {
            _bombCollider = GetComponent<Collider2D>();
        }

        public void Initialize(Collider2D playerCollider)
        {
            _playerCollider = playerCollider;
            IgnorePlayerCollision();
        }

        public void RestorePlayerCollision() => SetPlayerCollisionIgnored(false);
        public void IgnorePlayerCollision() => SetPlayerCollisionIgnored(true);

        private void SetPlayerCollisionIgnored(bool ignored)
        {
            Physics2D.IgnoreCollision(_bombCollider, _playerCollider, ignored);
        }
    }
}
