using System;
using UnityEngine;

namespace ExploderGuy
{
    public class BombExploder : MonoBehaviour
    {
        [SerializeField] private float _explosionTimer = 2;
        [SerializeField] private GameObject _bombExplosionPrefab;

        private void Update()
        {
            _explosionTimer -= Time.deltaTime;

            Explode();
        }

        private void Explode()
        {
            if (_explosionTimer <= 0)
            {
                Instantiate(_bombExplosionPrefab, transform.position, transform.rotation);
                Destroy(gameObject);
            } 
        }
    }
}
