using UnityEngine;
using UnityEngine.Tilemaps;

namespace ExploderGuy
{
    public class LevelGenerator : MonoBehaviour
    {
        [SerializeField] private Tilemap _blockTilemap;
        [SerializeField] private Tile _hardBlockTile;

        private TileType[,] _tileTypes = new TileType[11, 13];

        private void Awake()
        {
            CreateInitialState();
        }

        private void Start()
        {
            // create soft blocks and enemies here
        }

        private void CreateInitialState()
        {
            int x = 0;
            int y = 0;

            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    Vector3Int location = new Vector3Int(x + 1, -y - 1);

                    _tileTypes[x, y] = TileType.HardBlock;
                    _blockTilemap.SetTile(location, _hardBlockTile);

                    x += 2;
                }

                y += 2;
                x = 0;
            }
        }

        private void CreateSoftBlocks()
        {

        }
    }

    public enum TileType { Empty, HardBlock, SoftBlock }
}
