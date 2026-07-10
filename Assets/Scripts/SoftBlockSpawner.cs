using UnityEngine;

namespace ExploderGuy
{
    public class SoftBlockSpawner : MonoBehaviour
    {
        [SerializeField] private SoftBlock _softBlock;
        
        private void Awake()
        {
            Instantiate(_softBlock, new Vector3(-4, 5, 0), Quaternion.identity);
        }
    }
}
