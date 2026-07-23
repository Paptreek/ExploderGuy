using System;
using UnityEngine;

namespace ExploderGuy
{
    public class Bomb : MonoBehaviour
    {
        [SerializeField] private float _fuseTimer = 2f;
        [SerializeField] private BombExplosion _bombExplosionPrefab;

        private Collider2D _bombCollider;
        private Collider2D _playerCollider;
        private int _blastRadius;

        public event Action<Bomb> Exploded;

        private void Awake()
        {
            _bombCollider = GetComponent<Collider2D>();
        }

        private void Update()
        {
            _fuseTimer -= Time.deltaTime;
            if (_fuseTimer <= 0)
            {
                Explode();
            }
        }

        public void Initialize(BombPlacementContext context)
        {
            _playerCollider = context.OwnerCollider;
            _blastRadius = context.BlastRadius;
            IgnorePlayerCollision();
        }

        private void Explode()
        {
            BombExplosion explosion = Instantiate(_bombExplosionPrefab, transform.position, transform.rotation);
            explosion.Initialize(_blastRadius);
            Exploded?.Invoke(this);
            Destroy(gameObject);
        }

        public void RestorePlayerCollision() => SetPlayerCollisionIgnored(false);
        public void IgnorePlayerCollision() => SetPlayerCollisionIgnored(true);

        private void SetPlayerCollisionIgnored(bool ignored)
        {
            Physics2D.IgnoreCollision(_bombCollider, _playerCollider, ignored);
        }
    }
}
