using UnityEngine;
using UnityEngine.Tilemaps;

namespace ExploderGuy.PlayArea
{
    public class LevelGenerator : MonoBehaviour
    {
        [SerializeField] private Tilemap _blockTilemap;
        [SerializeField] private Tile _hardBlockTile;
        [SerializeField] private SoftBlock _softBlock;

        [SerializeField] private Pathfinding _pathfinding;

        private int _softBlockCount;
        private int _extraHardBlockCount;
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
            PlaceAdditionalHardBlocks();

            if (_extraHardBlockCount >= 9)
            {
                PlaceSoftBlocks();
            }
        }

        private void CreateInitialState()
        {
            int x = 0;
            int y = 0;

            for (int i = 0; i < 11; i++)
            {
                for (int j = 0; j < 13; j++)
                {
                    Vector3Int location = new Vector3Int(x, y);

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

        private void PlaceSoftBlocks()
        {
            int x = 0;
            int y = 0;

            while (_softBlockCount < 33)
            {
                for (int i = 0; i < 11; i++)
                {
                    for (int j = 0; j < 13; j++)
                    {
                        int random = Random.Range(1, 11);

                        if (_tileTypes[x, y] == TileType.Empty && random == 10 && _softBlockCount < 33)
                        {
                            Instantiate(_softBlock, new Vector3(x - 6, y - 5), Quaternion.identity);
                            _tileTypes[x, y] = TileType.SoftBlock;
                            _softBlockCount++;
                        }

                        x++;
                    }

                    y++;
                    x = 0;
                }

                x = 0;
                y = 0;
            }

            Debug.Log($"SoftBlocks: {_softBlockCount}");
        }

        private void PlaceAdditionalHardBlocks()
        {
            int x = 0;
            int y = 0;

            while (_extraHardBlockCount < 8)
            {
                for (int i = 0; i < 11; i++)
                {
                    for (int j = 0; j < 13; j++)
                    {
                        Vector3Int location = new Vector3Int(x, y);

                        int random = Random.Range(1, 14);

                        if (_tileTypes[x, y] == TileType.Empty && _extraHardBlockCount < 8 && random == 13 && IsClearOfTrappingPlayer(x, y))
                        {
                            _tileTypes[x, y] = TileType.HardBlock;
                            _blockTilemap.SetTile(location, _hardBlockTile);
                            _extraHardBlockCount++;
                        }

                        x++;
                    }

                    y++;
                    x = 0;
                }

                y = 0;
                x = 0;
            }

            Debug.Log($"Extra HardBlocks: {_extraHardBlockCount}");
        }

        private bool IsClearOfTrappingPlayer(int x, int y) // this helps prevent some issues, but it needs to be much better and cleaner.
        {
            if (x >= 2 && x <= 10)
            {
                if (_tileTypes[x - 2, y] == TileType.Empty && _tileTypes[x + 2, y] == TileType.Empty)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else if (y >= 2 && y <= 8)
            {
                if (_tileTypes[x, y - 2] == TileType.Empty && _tileTypes[x, y + 2] == TileType.Empty)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else if (x >= 2)
            {
                if (_tileTypes[x - 2, y] == TileType.Empty)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else if (x <= 10)
            {
                if (_tileTypes[x + 2, y] == TileType.Empty)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else if (y >= 2)
            {
                if (_tileTypes[x, y - 2] == TileType.Empty)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else if (y <= 8)
            {
                if (_tileTypes[x, y + 2] == TileType.Empty)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
    }

    public enum TileType { Empty, HardBlock, SoftBlock, SpawnPoint }
}
