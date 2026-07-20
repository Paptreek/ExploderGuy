using System.Collections.Generic;
using UnityEngine;

namespace ExploderGuy.PlayArea
{
    public class Pathfinding : MonoBehaviour
    {
        private NodeGrid _grid;

        [field: SerializeField] public bool PathIsBlocked { get; private set; }
        [field: SerializeField] public Transform Seeker { get; set; }
        [field: SerializeField] public Transform Target { get; set; }

        private void Awake()
        {
            _grid = GetComponent<NodeGrid>();
        }

        private void Update()
        {
            FindPath(Seeker.position, Target.position);

            if (PathIsBlocked)
            {
                _grid.NodePath.Clear();
            }
        }

        public void FindPath(Vector3 startPos, Vector3 targetPos)
        {
            Node startNode = _grid.GetNodeFromWorldPosition(startPos);
            Node targetNode = _grid.GetNodeFromWorldPosition(targetPos);

            List<Node> openSet = new List<Node>();
            HashSet<Node> closedSet = new HashSet<Node>();

            openSet.Add(startNode);

            while (openSet.Count > 0)
            {
                Node currentNode = openSet[0];

                for (int i = 1; i < openSet.Count; i++)
                {
                    if (openSet[i].FCost < currentNode.FCost || openSet[i].FCost == currentNode.FCost && openSet[i].HCost < currentNode.HCost)
                    {
                        currentNode = openSet[i];
                    }
                }

                openSet.Remove(currentNode);
                closedSet.Add(currentNode);

                if (currentNode == targetNode)
                {
                    RetracePath(startNode, targetNode);
                    return;
                }

                foreach (Node neighbor in _grid.GetNeighborNodeWithoutDiagonals(currentNode))
                {
                    if (!neighbor.Walkable || closedSet.Contains(neighbor))
                    {
                        continue;
                    }

                    int newMovementCostToNeighbor = currentNode.GCost + GetDistanceBetweenNodes(currentNode, neighbor);

                    if (newMovementCostToNeighbor < neighbor.GCost || !openSet.Contains(neighbor))
                    {
                        neighbor.GCost = newMovementCostToNeighbor;
                        neighbor.HCost = GetDistanceBetweenNodes(neighbor, targetNode);
                        neighbor.Parent = currentNode;

                        if (!openSet.Contains(neighbor))
                        {
                            openSet.Add(neighbor);
                        }
                    }
                }
            }

            if (startNode != targetNode)
            {
                PathIsBlocked = true;
            }
        }

        private void RetracePath(Node startNode, Node endNode)
        {
            List<Node> path = new List<Node>();
            Node currentNode = endNode;

            while (currentNode != startNode)
            {
                path.Add(currentNode);
                currentNode = currentNode.Parent;
                PathIsBlocked = false;
            }

            path.Reverse();

            _grid.NodePath = path;
        }

        private int GetDistanceBetweenNodes(Node nodeA, Node nodeB)
        {
            int xDistance = Mathf.Abs(nodeA.X - nodeB.X);
            int yDistance = Mathf.Abs(nodeA.Y - nodeB.Y);

            if (xDistance > yDistance)
            {
                return 14 * yDistance + 10 * (xDistance - yDistance);
            }
            else
            {
                return 14 * xDistance + 10 * (yDistance - xDistance);
            }
        }
    }
}
