using UnityEngine;

namespace ExploderGuy
{
    public class BombExplosion : MonoBehaviour
    {
        [SerializeField] private BombExplosionSegment[] _explosionSegments;

        public void Initialize(int blastRadius)
        {
            SetExplosionSegmentLength(blastRadius);
        }

        private void SetExplosionSegmentLength(int length)
        {
            foreach (BombExplosionSegment segment in _explosionSegments)
            {
                segment.SetLength(length);
            }
        }
    }
}
