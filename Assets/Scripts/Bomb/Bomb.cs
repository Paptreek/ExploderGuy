using System;
using UnityEngine;

namespace ExploderGuy
{
    public class Bomb : MonoBehaviour
    {
        [SerializeField] private float _explosionTimer = 2f;
        [SerializeField] private GameObject _bombExplosionPrefab;
        [SerializeField] private int _explosionRange;

        private Collider2D _bombCollider;
        private Collider2D _playerCollider;

        public event Action<Bomb> Exploded;

        private void Awake()
        {
            _bombCollider = GetComponent<Collider2D>();
        }

        private void Update()
        {
            _explosionTimer -= Time.deltaTime;
            if (_explosionTimer <= 0)
            {
                Explode();
            }
        }

        public void Initialize(Collider2D playerCollider)
        {
            _playerCollider = playerCollider;
            IgnorePlayerCollision();
        }

        private void Explode()
        {
            Instantiate(_bombExplosionPrefab, transform.position, transform.rotation);
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
