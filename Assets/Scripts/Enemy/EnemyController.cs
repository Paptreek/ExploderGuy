using ExploderGuy.PlayArea;
using UnityEngine;

namespace ExploderGuy
{
    public class EnemyController : MonoBehaviour
    {
        private LevelGenerator _levelGenerator;
        private int _positionX;
        private int _positionY;
        private float _moveSpeed = 0.5f;

        private Vector2Int _leftTileLocation;
        private Vector2Int _topTileLocation;
        private Vector2Int _rightTileLocation;
        private Vector2Int _bottomTileLocation;
        private TileType _leftTileType = TileType.HardBlock;
        private TileType _topTileType = TileType.HardBlock;
        private TileType _rightTileType = TileType.HardBlock;
        private TileType _bottomTileType = TileType.HardBlock;

        // temp for movement testing
        private bool _canMoveLeft;
        private bool _canMoveUp;
        private bool _canMoveRight;
        private bool _canMoveDown;

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
            if (_positionX - 1 >= 0)
            {
                _leftTileLocation = new Vector2Int(_positionX - 1, _positionY);
                _leftTileType = _levelGenerator.GetTileType(_leftTileLocation.x, _leftTileLocation.y);
            }

            if (_positionX + 1 <= 12)
            {
                _rightTileLocation = new Vector2Int(_positionX + 1, _positionY);
                _rightTileType = _levelGenerator.GetTileType(_rightTileLocation.x, _rightTileLocation.y);
            }

            if (_positionY + 1 <= 10)
            {
                _topTileLocation = new Vector2Int(_positionX, _positionY + 1);
                _topTileType = _levelGenerator.GetTileType(_topTileLocation.x, _topTileLocation.y);
            }

            if (_positionY - 1 >= 0)
            {
                _bottomTileLocation = new Vector2Int(_positionX, _positionY - 1);
                _bottomTileType = _levelGenerator.GetTileType(_bottomTileLocation.x, _bottomTileLocation.y);
            }

            //Debug.Log($"Left: {_leftTileType}, {_leftTileLocation}, " +
            //          $"Top: {_topTileType}, {_topTileLocation}, " +
            //          $"Right: {_rightTileType}, {_rightTileLocation}, " +
            //          $"Bottom: {_bottomTileType}, {_bottomTileLocation}");

            ChooseRandomMovementDirection();
        }

        private void ChooseRandomMovementDirection()
        {
            if (_leftTileType == TileType.Empty)
            {
                //Debug.Log($"{_leftTileLocation} is {_leftTileType} (left)!");
                _canMoveLeft = true;
            }
            else if (_topTileType == TileType.Empty)
            {
                //Debug.Log($"{_topTileLocation} is {_topTileType} (top)");
                _canMoveUp = true;
            }
            else if (_rightTileType == TileType.Empty)
            {
                //Debug.Log($"{_rightTileLocation} is {_rightTileType} (right)");
                _canMoveRight = true;
            }
            else if (_bottomTileType == TileType.Empty)
            {
                //Debug.Log($"{_bottomTileLocation} is {_bottomTileType} (bottom)");
                _canMoveDown = true;
            }
        }

        private void Move()
        {
            if (_canMoveLeft)
            {
                transform.Translate(new Vector3(-_moveSpeed * Time.deltaTime, 0));
            }

            if (_canMoveUp)
            {
                transform.Translate(new Vector3(0, _moveSpeed * Time.deltaTime));
            }

            if (_canMoveRight)
            {
                transform.Translate(new Vector3(_moveSpeed * Time.deltaTime, 0));
            }

            if (_canMoveDown)
            {
                transform.Translate(new Vector3(0, -_moveSpeed * Time.deltaTime));
            }
        }
    }

    public enum DirectionToMove { None, Left, Up, Right, Down }
}
