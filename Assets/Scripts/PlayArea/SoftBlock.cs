using UnityEngine;

namespace ExploderGuy.PlayArea
{
    public class SoftBlock : MonoBehaviour
    {
        public int X { get; set; }
        public int Y { get; set; }
        public bool IsDestroyed { get; private set; }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag($"Explosion"))
            {
                IsDestroyed = true;
                Destroy(gameObject);
            }
        }
    }
}
