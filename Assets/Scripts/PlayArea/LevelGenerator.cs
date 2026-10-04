using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace ExploderGuy.PlayArea
{
    public class LevelGenerator : MonoBehaviour
    {
        [SerializeField] private Tilemap _blockTilemap;
        [SerializeField] private Tile _hardBlockTile;
        [SerializeField] private SoftBlock _softBlock;
        [SerializeField] private Transform _exitPortalTransform;
        [SerializeField] private EnemyController _enemy;

        [SerializeField] private Pathfinding _pathfinding;
        [SerializeField] private NodeGrid _nodeGrid;

        private int _extraHardBlockCount;
        private List<EnemyController> _enemies = new List<EnemyController>();
        private List<SoftBlock> _softBlocks = new List<SoftBlock>();
        private TileType[,] _tileTypes = new TileType[13, 11];

        private void Awake()
        {
            _blockTilemap.ClearAllTiles();

            GameObject[] softBlocks = GameObject.FindGameObjectsWithTag("SoftBlock");
            foreach (GameObject softBlock in softBlocks)
            {
                Destroy(softBlock);
            }

            CreateInitialState();
        }

        private void Start()
        {
            PlaceAdditionalHardBlocks();
        }

        private void Update()
        {
            if (_softBlocks.Count >= 33)
            {
                PlaceEnemies();
            }

            UpdateSoftBlockTileTypes();
        }

        public TileType GetTileType(int x, int y)
        {
            return _tileTypes[x, y];
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

            _tileTypes[0, 10] = TileType.SpawnPoint;
            _tileTypes[1, 10] = TileType.SpawnPoint;
            _tileTypes[0, 9] = TileType.SpawnPoint;
        }

        private void PlaceSoftBlocks()
        {
            int x = 0;
            int y = 0;

            while (_softBlocks.Count < 33)
            {
                for (int i = 0; i < 11; i++)
                {
                    for (int j = 0; j < 13; j++)
                    {
                        int random = Random.Range(1, 14);

                        if (_tileTypes[x, y] == TileType.Empty && random == 13 && _softBlocks.Count < 33)
                        {
                            SoftBlock softBlock = Instantiate(_softBlock, new Vector3(x - 6, y - 5), Quaternion.identity);
                            softBlock.X = x;
                            softBlock.Y = y;
                            _tileTypes[x, y] = TileType.SoftBlock;
                            _softBlocks.Add(softBlock);
                        }

                        x++;
                    }

                    y++;
                    x = 0;
                }

                x = 0;
                y = 0;
            }

            Debug.Log($"SoftBlocks: {_softBlocks.Count}");
            PlaceExitPortal();
        }

        private async void PlaceAdditionalHardBlocks()
        {
            CancellationTokenSource tokenSource = new CancellationTokenSource();

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

                            await CheckForDeadEnds(x, y, tokenSource.Token);
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
            _nodeGrid.ShowDebug = false;
        }

        private async Task CheckForDeadEnds(int x, int y, CancellationToken token)
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
                            if (!EditorApplication.isPlaying)
                            {
                                Debug.Log($"Not playing");

                                token.ThrowIfCancellationRequested();
                            }
                            else
                            {
                                await Task.Yield();
                                _pathfinding.FindPath(_pathfinding.Seeker.position, new Vector3(xToCheck - 6, yToCheck - 5));

                                if (_pathfinding.PathIsBlocked)
                                {
                                    Debug.Log("Found a dead end. Starting over!");
                                    _blockTilemap.ClearAllTiles();

                                    ResetTileTypes();

                                    _extraHardBlockCount = 0;
                                    CreateInitialState();
                                }
                            }

                        }
                    }
                }
            }
        }

        private void PlaceExitPortal()
        {
            int random = Random.Range(0, _softBlocks.Count);
            _exitPortalTransform.position = _softBlocks[random].transform.position;
        }

        private void PlaceEnemies()
        {
            while (_enemies.Count < 3)
            {
                int randomX = Random.Range(0, 13);
                int randomY = Random.Range(0, 11);

                if (_tileTypes[randomX, randomY] == TileType.Empty)
                {
                    EnemyController enemy = Instantiate(_enemy, new Vector3(randomX - 6, randomY - 5), Quaternion.identity);
                    enemy.SetLevelGenerator(this);
                    _tileTypes[randomX, randomY] = TileType.Enemy;
                    _enemies.Add(enemy);
                }
            }
        }

        private void ResetTileTypes()
        {
            for (int rows = 0; rows < 13; rows++)
            {
                for (int columns = 0; columns < 11; columns++)
                {
                    _tileTypes[rows, columns] = TileType.Empty;
                }
            }
        }

        private void UpdateSoftBlockTileTypes()
        {
            foreach (SoftBlock block in _softBlocks)
            {
                if (block.IsDestroyed)
                {
                    _tileTypes[block.X, block.Y] = TileType.Empty;
                }
            }
        }
    }

    public enum TileType { Empty, HardBlock, SoftBlock, SpawnPoint, Enemy }
}
