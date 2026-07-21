using System;
using UnityEngine;

namespace ExploderGuy
{
    public class PlayerState : MonoBehaviour
    {
        [SerializeField] private int _startingLives = 3;
        [SerializeField] private int _maximumLives = 9;
        [SerializeField] private int _startingActiveBombLimit = 1;
        [SerializeField] private float _defaultInvulnerabilityDuration = 10f;
        [SerializeField] private float _baseMoveSpeed = 3f;

        private Vector3 _startingPosition;
        private PlayerInvulnerability _invulnerability;

        public int LivesRemaining { get; private set; }
        public int ActiveBombLimit { get; private set; }
        public int MoveSpeedLevel { get; private set; }
        public float MoveSpeed => _baseMoveSpeed * MoveSpeedLevel;

        private void Awake()
        {
            _invulnerability = GetComponent<PlayerInvulnerability>();
            _startingPosition = transform.position;
            InitializePlayerState();
        }

        private void Start()
        {
            SpawnPlayer();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag($"Explosion"))
            {
                RespawnPlayer();
            }
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("Enemy"))
            {
                RespawnPlayer();
            }
        }

        private void InitializePlayerState()
        {
            LivesRemaining = _startingLives;
            ActiveBombLimit = _startingActiveBombLimit;
            MoveSpeedLevel = 1;
        }

        public void RespawnPlayer()
        {
            LoseLife();
            SpawnPlayer();
        }

        private void SpawnPlayer()
        {
            transform.position = _startingPosition;
            _invulnerability.SetInvulnerability(_defaultInvulnerabilityDuration);
        }

        public void LoseLife()
        {
            LivesRemaining = Mathf.Max(0, LivesRemaining - 1);
            ReduceMoveSpeedLevel();
        }
        public void AddLife() => LivesRemaining = Mathf.Min(LivesRemaining + 1, _maximumLives);
        public void AddMoveSpeedLevel() => MoveSpeedLevel++;

        public void ReduceMoveSpeedLevel() => MoveSpeedLevel = Mathf.Max(1, MoveSpeedLevel - 1);
        public void IncreaseBombLimit() => ActiveBombLimit++;
    }
}
