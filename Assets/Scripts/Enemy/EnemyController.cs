using ExploderGuy.PlayArea;
using Mono.Cecil.Cil;
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
        private bool _canMoveUp;

        private void Start()
        {
            _positionX = Mathf.FloorToInt(transform.position.x + 6);
            _positionY = Mathf.FloorToInt(transform.position.y + 5);

            FindAdjacentEmptyTile();
        }

        private void Update()
        {
            Move();
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

        private void FindAdjacentEmptyTile()
        {
            if (_positionX - 1 >= 0 && _positionY + 1 <= 10)
            {
                Vector2Int leftTileLocation = new Vector2Int(_positionX - 1, _positionY);
                Vector2Int topTileLocation = new Vector2Int(_positionX, _positionY + 1);

                TileType leftTileType = _levelGenerator.GetTileType(leftTileLocation.x, leftTileLocation.y);
                TileType topTileType = _levelGenerator.GetTileType(topTileLocation.x, topTileLocation.y);

                if (leftTileType == TileType.Empty)
                {
                    Debug.Log($"{leftTileLocation} is {leftTileType} (left)!");
                    _canMoveLeft = true;
                }

                // TODO: perhaps can use a bool for this instead of making sure other tiles aren't empty?
                if (leftTileType != TileType.Empty && topTileType == TileType.Empty)
                {
                    Debug.Log($"{topTileLocation} is {topTileType} (top)");
                    _canMoveUp = true;
                }
            }
        }

        private void ChooseRandomMovementDirection()
        {

        }

        private void Move()
        {
            if (_canMoveLeft)
            {
                transform.Translate(new Vector3(-0.5f * Time.deltaTime, 0));
            }

            if (_canMoveUp)
            {
                transform.Translate(new Vector3(0, 0.5f * Time.deltaTime));
            }
        }
    }

    public enum DirectionToMove { None, Left, Up, Right, Down }
}
