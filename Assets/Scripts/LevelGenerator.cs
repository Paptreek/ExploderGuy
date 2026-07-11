using UnityEngine;
using UnityEngine.Tilemaps;

namespace ExploderGuy
{
    public class LevelGenerator : MonoBehaviour
    {
        [SerializeField] private Tilemap _blockTilemap;
        [SerializeField] private Tile _hardBlockTile;

        [SerializeField] private SoftBlock _softBlock;

        private int _softBlockCount;
        private TileType[,] _tileTypes = new TileType[13, 11];

        private void Awake()
        {
            CreateInitialState();

            _tileTypes[0, 10] = TileType.SpawnPoint;
            _tileTypes[1, 10] = TileType.SpawnPoint;
            _tileTypes[0, 9] = TileType.SpawnPoint;
        }

        private void Start()
        {
            CreateSoftBlocks();
        }

        private void CreateInitialState()
        {
            int x = 0;
            int y = 0;

            for (int i = 0; i < 11; i++)
            {
                for (int j = 0; j < 13; j++)
                {
                    Vector3Int location = new Vector3Int(x, -y);

                    if (i % 2 == 1 && j % 2 == 1)
                    {
                        _tileTypes[x, y] = TileType.HardBlock;
                        _blockTilemap.SetTile(location, _hardBlockTile);
                    }

                    x++;
                }

                y++;
                x = 0;
            }
        }

        private void CreateSoftBlocks()
        {
            int x = 0;
            int y = 0;

            while (_softBlockCount < 33)
            {
                for (int i = 0; i < 11; i++)
                {
                    for (int j = 0; j < 13; j++)
                    {
                        if (_tileTypes[x, y] != TileType.HardBlock && _tileTypes[x, y] != TileType.SpawnPoint)
                        {
                            int random = Random.Range(1, 11);

                            if (random == 10 && _softBlockCount < 33)
                            {
                                Instantiate(_softBlock, new Vector3(x - 6, y - 5), Quaternion.identity);
                                _tileTypes[x, y] = TileType.SoftBlock;
                                _softBlockCount++;
                            }
                        }

                        x++;
                    }

                    y++;
                    x = 0;
                }

                x = 0;
                y = 0;
            }

            Debug.Log($"Soft Block Count: {_softBlockCount}");
        }
    }

    public enum TileType { Empty, HardBlock, SoftBlock, SpawnPoint }
}
