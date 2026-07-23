using UnityEngine;

namespace ExploderGuy
{
    public readonly struct BombPlacementContext
    {
        public Collider2D OwnerCollider { get; }
        public int BlastRadius { get; }

        public BombPlacementContext(
            Collider2D ownerCollider,
            int blastRadius)
        {
            OwnerCollider = ownerCollider;
            BlastRadius = blastRadius;
        }
    }
}
