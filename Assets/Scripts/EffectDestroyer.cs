using UnityEngine;

namespace ExploderGuy
{
    public class EffectDestroyer : MonoBehaviour
    {
        private void Awake()
        {
            Destroy(gameObject, 0.5f);
        }
    }
}
