using ExploderGuy.PlayArea;
using UnityEngine;

namespace ExploderGuy
{
    public class EnemyController : MonoBehaviour
    {
        private LevelGenerator _levelGenerator;
        private int _positionX;
        private int _positionY;

        // temp for movement testing
        private bool _canMoveLeft;

        private void Start()
        {
            _positionX = Mathf.FloorToInt(transform.position.x + 6);
            _positionY = Mathf.FloorToInt(transform.position.y + 5);

            ChooseAdjacentEmptyTile();
        }

        private void Update()
        {
            // temp for movement testing
            if (_canMoveLeft)
            {
                Move();
            }
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

        private void ChooseAdjacentEmptyTile()
        {
            if (_positionX - 1 >= 0)
            {
                Vector2Int leftTileLocation = new Vector2Int(_positionX - 1, _positionY);
                TileType leftTileType = _levelGenerator.GetTileType(leftTileLocation.x, leftTileLocation.y);

                if (leftTileType == TileType.Empty)
                {
                    Debug.Log($"{leftTileLocation} is {leftTileType}!");
                    _canMoveLeft = true;
                }

            }
        }

        private void ChooseRandomMovementDirection()
        {

        }

        private void Move()
        {
            // temp for movement testing
            transform.Translate(new Vector3(-1 * Time.deltaTime, 0));
        }
    }

    public enum DirectionToMove { None, Left, Up, Right, Down }
}
