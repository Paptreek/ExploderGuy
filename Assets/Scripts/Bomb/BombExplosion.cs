using UnityEngine;

namespace ExploderGuy
{
    public class BombExplosion : MonoBehaviour
    {
        [SerializeField] private ExplosionSegment[] _explosionSegments;

        public void Initialize(int blastRadius)
        {
            SetExplosionSegmentLength(blastRadius);
        }

        private void SetExplosionSegmentLength(int length)
        {
            foreach (ExplosionSegment segment in _explosionSegments)
            {
                segment.SetLength(length);
            }
        }
    }
}
