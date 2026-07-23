using UnityEngine;

namespace ExploderGuy
{
    public class ExplosionSegment : MonoBehaviour
    {
        public void SetLength(int length)
        {
            transform.localScale = new Vector3(1, length, 1);
        }
    }
}
