using UnityEngine;

namespace ExploderGuy.PlayArea
{
    public class SoftBlock : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag($"Explosion"))
            {
                Destroy(gameObject);
            }
        }
    }
}
