using ExploderGuy.PlayArea;
using UnityEngine;

namespace ExploderGuy
{
    public class EnemyController : MonoBehaviour
    {
        private LevelGenerator _levelGenerator;

        private void Start()
        {
            int positionX = Mathf.FloorToInt(transform.position.x + 6);
            int positionY = Mathf.FloorToInt(transform.position.y + 5);

            if (positionX + 1 > 0 && positionY + 1 > 0 && positionX + 1 < 13 && positionY < 11)
            {
                Debug.Log(_levelGenerator.GetTileType(positionX + 1, positionY));
            }
        }

        private void Update()
        {

        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag($"Explosion"))
            {
                Destroy(gameObject);
            }
        }

        public void SetLevelGenerator(LevelGenerator levelGenerator)
        {
            _levelGenerator = levelGenerator;
        }

        private void FindAdjacentEmptyTiles()
        {
            
        }

        private void ChooseRandomMovementDirection()
        {

        }

        private void Move()
        {

        }
    }
}
