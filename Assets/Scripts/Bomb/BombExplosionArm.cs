using UnityEngine;

namespace ExploderGuy
{
    public class BombExplosionArm : MonoBehaviour
    {
        void Update()
        {
            transform.localScale += new Vector3(0, -0.5f, 0) * Time.deltaTime;
        }
    }
}
