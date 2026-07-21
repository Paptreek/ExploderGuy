using UnityEngine;

namespace ExploderGuy
{
    public class ExitPortal : MonoBehaviour
    {
        public bool AllEnemiesCleared { get; set; } // set this in GameManager when enemies are all dead

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player") /*&& AllEnemiesCleared*/)
            {
                Debug.Log($"Level Complete!");
            }
        }
    }
}
