using UnityEngine;

namespace ExploderGuy
{
    public class DestroyOnExplosion : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.CompareTag("Explosion"))
            {
                return;
            }

            Destroy(gameObject);
        }
    }
}
