using UnityEngine;

namespace ExploderGuy
{
    public class BombExplosionSegment : MonoBehaviour
    {
        public void SetLength(int length)
        {
            transform.localScale = new Vector3(length, 1, 1);
        }
    }
}
