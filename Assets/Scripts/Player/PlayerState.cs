using UnityEngine;

namespace ExploderGuy
{
    public class PlayerState : MonoBehaviour
    {
        [SerializeField] private int _startingLives = 3;
        [SerializeField] private int _maximumLives = 9;
        [SerializeField] private int _startingActiveBombLimit = 1;
        [SerializeField] private float _defaultInvulnerabilityDuration = 10f;

        private Vector3 _startingPosition;
        private PlayerInvulnerability _invulnerability;

        public int LivesRemaining { get; private set; }
        public int ActiveBombLimit { get; private set; }

        private void Awake()
        {
            _invulnerability = GetComponent<PlayerInvulnerability>();
            _startingPosition = transform.position;
            ResetPlayerState();
            InitialPlayerSpawn();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!_invulnerability.IsInvulnerable && collision.CompareTag($"Explosion"))
            {
                RespawnPlayer();
            }
        }

        public void ResetPlayerState()
        {
            _invulnerability.ClearInvulnerability();
            LivesRemaining = _startingLives;
            ActiveBombLimit = _startingActiveBombLimit;
        }

        public void InitialPlayerSpawn() => SpawnPlayer();

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

        public void LoseLife() => LivesRemaining = Mathf.Max(0, LivesRemaining - 1);
        public void AddLife() => LivesRemaining = Mathf.Min(LivesRemaining + 1, _maximumLives);
        public void IncreaseBombLimit() => ActiveBombLimit++;
    }
}
