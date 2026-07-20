using UnityEngine;

namespace ExploderGuy.PlayArea
{
    public class Node
    {
        public Node Parent { get; set; }
        public int GCost { get; set; }
        public int HCost { get; set; }
        public int FCost => GCost + HCost;

        public bool Walkable { get; set; }
        public Vector3 WorldPosition { get; private set; }
        public int X { get; private set; }
        public int Y { get; private set; }


        public Node(bool walkable, Vector3 worldPosition, int x, int y)
        {
            Walkable = walkable;
            WorldPosition = worldPosition;
            X = x;
            Y = y;
        }
    }
}
