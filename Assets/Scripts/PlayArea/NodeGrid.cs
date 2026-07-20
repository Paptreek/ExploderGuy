using System.Collections.Generic;
using UnityEngine;

namespace ExploderGuy.PlayArea
{
    public class NodeGrid : MonoBehaviour
    {
        private bool _walkable;
        private float _nodeDiameter;
        private int _gridSizeX, _gridSizeY;
        private Vector3 _worldBottomLeft;
        private Node[,] _nodeGrid;

        [field: SerializeField] public Vector2 WorldSize { get; set; }
        [field: SerializeField] public float NodeRadius { get; set; }

        public List<Node> NodePath { get; set; }

        private void Start()
        {
            CreateGrid();
        }

        private void Update()
        {
            for (int x = 0; x < _gridSizeX; x++)
            {
                for (int y = 0; y < _gridSizeY; y++)
                {
                    _nodeGrid[x, y].Walkable = !Physics2D.BoxCast(_nodeGrid[x, y].WorldPosition, new Vector2(0.9f, 0.9f), 0, Vector2.zero);
                }
            }
        }

        public void AddNode(int x, int y)
        {
            Vector3 worldPoint = GetWorldPoint(x, y);
            _walkable = !Physics2D.BoxCast(worldPoint, new Vector2(0.9f, 0.9f), 0, Vector2.zero);
            _nodeGrid[x, y] = new Node(_walkable, worldPoint, x, y);
        }

        public List<Node> GetNeighborNode(Node currentNode)
        {
            List<Node> neighbors = new List<Node>();

            for (int x = -1; x <= 1; x++)
            {
                for (int y = -1; y <= 1; y++)
                {
                    if (x == 0 && y == 0)
                    {
                        continue;
                    }

                    int xToCheck = currentNode.X + x;
                    int yToCheck = currentNode.Y + y;

                    if (xToCheck >= 0 && xToCheck < _gridSizeX && yToCheck >= 0 && yToCheck < _gridSizeY)
                    {
                        neighbors.Add(_nodeGrid[xToCheck, yToCheck]);
                    }
                }
            }

            return neighbors;
        }

        public List<Node> GetNeighborNodeWithoutDiagonals(Node currentNode)
        {
            List<Node> neighbors = new List<Node>();

            for (int x = -1; x <= 1; x++)
            {
                for (int y = -1; y <= 1; y++)
                {
                    if (x == 0 && y == 0 || Mathf.Abs(x) == 1 && Mathf.Abs(y) == 1)
                    {
                        continue;
                    }

                    int xToCheck = currentNode.X + x;
                    int yToCheck = currentNode.Y + y;

                    if (xToCheck >= 0 && xToCheck < _gridSizeX && yToCheck >= 0 && yToCheck < _gridSizeY)
                    {
                        neighbors.Add(_nodeGrid[xToCheck, yToCheck]);
                    }
                }
            }

            return neighbors;
        }

        public Node GetNodeFromWorldPosition(Vector3 worldPosition)
        {
            float worldPosAsPercentX = Mathf.Clamp01((worldPosition.x + WorldSize.x / 2) / WorldSize.x);
            float worldPosAsPercentY = Mathf.Clamp01((worldPosition.y + WorldSize.y / 2) / WorldSize.y);

            int x = Mathf.RoundToInt((_gridSizeX - 1) * worldPosAsPercentX);
            int y = Mathf.RoundToInt((_gridSizeY - 1) * worldPosAsPercentY);

            return _nodeGrid[x, y];
        }

        private void CreateGrid()
        {
            _nodeDiameter = NodeRadius * 2;
            _gridSizeX = Mathf.RoundToInt(WorldSize.x / _nodeDiameter);
            _gridSizeY = Mathf.RoundToInt(WorldSize.y / _nodeDiameter);

            _worldBottomLeft = transform.position - Vector3.right * WorldSize.x / 2 - Vector3.up * WorldSize.y / 2;

            _nodeGrid = new Node[_gridSizeX, _gridSizeY];

            for (int x = 0; x < _gridSizeX; x++)
            {
                for (int y = 0; y < _gridSizeY; y++)
                {
                    AddNode(x, y);
                }
            }
        }

        private Vector3 GetWorldPoint(int x, int y)
        {
            return _worldBottomLeft + Vector3.right * (x * _nodeDiameter + NodeRadius) + Vector3.up * (y * _nodeDiameter + NodeRadius);
        }

        private void OnDrawGizmos()
        {
            Color invisible = new Color32(255, 255, 255, 0);

            if (_nodeGrid != null)
            {
                foreach (Node node in _nodeGrid)
                {
                    Gizmos.color = node.Walkable ? invisible : invisible;

                    if (NodePath != null)
                    {
                        if (NodePath.Contains(node))
                        {
                            Debug.Log(NodePath.Count);
                            Gizmos.color = Color.red;
                        }
                    }

                    Gizmos.DrawCube(node.WorldPosition, Vector3.one * (_nodeDiameter - 0.5f));
                }
            }
        }
    }
}
