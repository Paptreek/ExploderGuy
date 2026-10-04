using System.Collections.Generic;
using ExploderGuy.PlayArea;
using UnityEngine;

namespace ExploderGuy
{
    public class EnemyController : MonoBehaviour
    {
        private LevelGenerator _levelGenerator;
        private Rigidbody2D _rb;

        private float _moveSpeed = 60.0f;
        private bool _canMoveLeft;
        private bool _canMoveUp;
        private bool _canMoveRight;
        private bool _canMoveDown;
        private bool _isMoving;

        private TileType _leftTileType = TileType.HardBlock;
        private TileType _topTileType = TileType.HardBlock;
        private TileType _rightTileType = TileType.HardBlock;
        private TileType _bottomTileType = TileType.HardBlock;

        public int X { get; private set; }
        public int Y { get; private set; }

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            X = Mathf.RoundToInt(transform.position.x + 6);
            Y = Mathf.RoundToInt(transform.position.y + 5);

            if (!_isMoving)
            {
                FindAdjacentEmptyTile();
            }
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

            if (X - 1 >= 0)
            {
                leftTileLocation = new Vector2Int(X - 1, Y);
                _leftTileType = _levelGenerator.GetTileType(leftTileLocation.x, leftTileLocation.y);
            }

            if (X + 1 <= 12)
            {
                rightTileLocation = new Vector2Int(X + 1, Y);
                _rightTileType = _levelGenerator.GetTileType(rightTileLocation.x, rightTileLocation.y);
            }

            if (Y + 1 <= 10)
            {
                topTileLocation = new Vector2Int(X, Y + 1);
                _topTileType = _levelGenerator.GetTileType(topTileLocation.x, topTileLocation.y);
            }

            if (Y - 1 >= 0)
            {
                bottomTileLocation = new Vector2Int(X, Y - 1);
                _bottomTileType = _levelGenerator.GetTileType(bottomTileLocation.x, bottomTileLocation.y);
            }

            SelectRandomMovementDirection();
        }

        private void SelectRandomMovementDirection()
        {
            List<DirectionToMove> possibleDirections = new List<DirectionToMove>();

            if (IsMoveable(_leftTileType))
            {
                possibleDirections.Add(DirectionToMove.Left);
            }

            if (IsMoveable(_topTileType))
            {
                possibleDirections.Add(DirectionToMove.Up);
            }

            if (IsMoveable(_rightTileType))
            {
                possibleDirections.Add(DirectionToMove.Right);
            }

            if (IsMoveable(_bottomTileType))
            {
                possibleDirections.Add(DirectionToMove.Down);
            }

            if (possibleDirections.Count > 0)
            {
                DirectionToMove chosenDirection = possibleDirections[Random.Range(0, possibleDirections.Count)];

                switch (chosenDirection)
                {
                    case DirectionToMove.Left:
                        _canMoveLeft = true;
                        break;
                    case DirectionToMove.Up:
                        _canMoveUp = true;
                        break;
                    case DirectionToMove.Right:
                        _canMoveRight = true;
                        break;
                    case DirectionToMove.Down:
                        _canMoveDown = true;
                        break;
                }
            }
        }

        private void Move()
        {
            if (_canMoveLeft)
            {
                SetMoveSettings(false, -_moveSpeed);
            }

            if (_canMoveUp)
            {
                SetMoveSettings(true, _moveSpeed);
            }

            if (_canMoveRight)
            {
                SetMoveSettings(false, _moveSpeed);
            }

            if (_canMoveDown)
            {
                SetMoveSettings(true, -_moveSpeed);
            }
        }

        private void SetMoveSettings(bool isMovingVertically, float moveSpeed)
        {
            if (isMovingVertically)
            {
                _rb.linearVelocityY = moveSpeed * Time.deltaTime;
                _rb.constraints = RigidbodyConstraints2D.FreezePositionX;
            }
            else
            {
                _rb.linearVelocityX = moveSpeed * Time.deltaTime;
                _rb.constraints = RigidbodyConstraints2D.FreezePositionY;
            }

            _isMoving = true;
        }

        private bool IsMoveable(TileType tile)
        {
            if (tile == TileType.Empty || tile == TileType.Enemy || tile == TileType.SpawnPoint)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    public enum DirectionToMove { None, Left, Up, Right, Down }
}
