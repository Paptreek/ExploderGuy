using ExploderGuy.PlayArea;
using UnityEngine;

namespace ExploderGuy
{
    public class EnemyController : MonoBehaviour
    {
        private LevelGenerator _levelGenerator;
        private Rigidbody2D _rb;

        private int _positionX;
        private int _positionY;
        private float _moveSpeed = 100.0f;
        private bool _canMoveLeft;
        private bool _canMoveUp;
        private bool _canMoveRight;
        private bool _canMoveDown;

        private TileType _leftTileType = TileType.HardBlock;
        private TileType _topTileType = TileType.HardBlock;
        private TileType _rightTileType = TileType.HardBlock;
        private TileType _bottomTileType = TileType.HardBlock;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
        }

        private void Start()
        {
            _positionX = Mathf.FloorToInt(transform.position.x + 6);
            _positionY = Mathf.FloorToInt(transform.position.y + 5);

            FindAdjacentEmptyTile();
        }

        private void Update()
        {
            _positionX = Mathf.FloorToInt(transform.position.x + 6);
            _positionY = Mathf.FloorToInt(transform.position.y + 5);

            FindAdjacentEmptyTile();
        }

        private void FixedUpdate()
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

        private void OnCollisionEnter2D(Collision2D collision)
        {
            _moveSpeed = -_moveSpeed;
        }

        public void SetLevelGenerator(LevelGenerator levelGenerator)
        {
            _levelGenerator = levelGenerator;
        }

        private void FindAdjacentEmptyTile()
        {
            Vector2Int leftTileLocation;
            Vector2Int topTileLocation;
            Vector2Int rightTileLocation;
            Vector2Int bottomTileLocation;

            if (_positionX - 1 >= 0)
            {
                leftTileLocation = new Vector2Int(_positionX - 1, _positionY);
                _leftTileType = _levelGenerator.GetTileType(leftTileLocation.x, leftTileLocation.y);
            }

            if (_positionX + 1 <= 12)
            {
                rightTileLocation = new Vector2Int(_positionX + 1, _positionY);
                _rightTileType = _levelGenerator.GetTileType(rightTileLocation.x, rightTileLocation.y);
            }

            if (_positionY + 1 <= 10)
            {
                topTileLocation = new Vector2Int(_positionX, _positionY + 1);
                _topTileType = _levelGenerator.GetTileType(topTileLocation.x, topTileLocation.y);
            }

            if (_positionY - 1 >= 0)
            {
                bottomTileLocation = new Vector2Int(_positionX, _positionY - 1);
                _bottomTileType = _levelGenerator.GetTileType(bottomTileLocation.x, bottomTileLocation.y);
            }

            SelectMovementDirection();
        }

        private void SelectMovementDirection()
        {
            if (_leftTileType == TileType.Empty)
            {
                _canMoveLeft = true;
            }
            else if (_topTileType == TileType.Empty)
            {
                _canMoveUp = true;
            }
            else if (_rightTileType == TileType.Empty)
            {
                _canMoveRight = true;
            }
            else if (_bottomTileType == TileType.Empty)
            {
                _canMoveDown = true;
            }
        }

        private void Move()
        {
            if (_canMoveLeft)
            {
                _rb.linearVelocityX = -_moveSpeed * Time.deltaTime;
                _rb.constraints = RigidbodyConstraints2D.FreezePositionY;
            }

            if (_canMoveUp)
            {
                _rb.linearVelocityY = _moveSpeed * Time.deltaTime;
                _rb.constraints = RigidbodyConstraints2D.FreezePositionX;
            }

            if (_canMoveRight)
            {
                _rb.linearVelocityX = _moveSpeed * Time.deltaTime;
                _rb.constraints = RigidbodyConstraints2D.FreezePositionY;
            }

            if (_canMoveDown)
            {
                _rb.linearVelocityY = -_moveSpeed * Time.deltaTime;
                _rb.constraints = RigidbodyConstraints2D.FreezePositionX;
            }
        }
    }

    public enum DirectionToMove { None, Left, Up, Right, Down }
}
