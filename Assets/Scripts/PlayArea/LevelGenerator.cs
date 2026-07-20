using System.Collections.Generic;
using System.Threading.Tasks;
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
        [SerializeField] private NodeGrid _nodeGrid;

        private int _softBlockCount;
        private int _extraHardBlockCount;
        private float _timer;
        private Vector3 _positionToCheck;
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

        private async void PlaceAdditionalHardBlocks()
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

                        if (_tileTypes[x, y] == TileType.Empty && _extraHardBlockCount < 8 && random == 13)
                        {
                            _tileTypes[x, y] = TileType.HardBlock;
                            _blockTilemap.SetTile(location, _hardBlockTile);
                            _extraHardBlockCount++;

                            await CheckForDeadEnds(x, y);
                        }

                        x++;
                    }

                    y++;
                    x = 0;
                }

                y = 0;
                x = 0;
            }

            Debug.Log($"Extra HardBlocks: {_extraHardBlockCount}. ALL BLOCKS PLACED!");
            PlaceSoftBlocks();
        }

        private async Task CheckForDeadEnds(int x, int y)
        {
            List<Vector3> positionsToCheck = new List<Vector3>();

            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    int xToCheck = x + i;
                    int yToCheck = y + j;

                    if (xToCheck >= 0 && yToCheck >= 0 && xToCheck < 13 && yToCheck < 11)
                    {
                        if (_tileTypes[xToCheck, yToCheck] == TileType.Empty)
                        {
                            //await Task.Delay(500);
                            await Task.Yield();
                            _pathfinding.FindPath(_pathfinding.Seeker.position, new Vector3(xToCheck - 6, yToCheck - 5));

                            if (_pathfinding.PathIsBlocked)
                            {
                                _blockTilemap.ClearAllTiles();
                                _extraHardBlockCount = 0;
                                CreateInitialState();
                            }
                        }
                    }
                }
            }
        }
    }

    public enum TileType { Empty, HardBlock, SoftBlock, SpawnPoint }
}
