using UnityEngine;

namespace ExploderGuy
{
    public class BombExplosion : MonoBehaviour
    {
        private int _blastRange;

        public void Initialize(int blastRange)
        {
            _blastRange = blastRange;
        }
    }
}
