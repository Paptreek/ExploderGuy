using UnityEngine;

namespace ExploderGuy.Player
{
    public class PlayerState : MonoBehaviour
    {
        [SerializeField] private int _startingLives = 3;
        [SerializeField] private int _maximumLives = 9;
        [SerializeField] private int _startingActiveBombLimit = 1;
        [SerializeField] private float _defaultInvulnerabilityDuration = 10f;

        private float _invulnerabilityTimer = 0f;
        private Vector3 _startingPosition;

        public bool IsInvulnerable { get; private set; }
        public int LivesRemaining { get; private set; }
        public int ActiveBombLimit { get; private set; }

        private void Awake()
        {
            _startingPosition = transform.position;
            ResetPlayerState();
        }

        private void Update()
        {
            UpdateInvulnerability();
        }

        private void UpdateInvulnerability()
        {
            if (!IsInvulnerable)
            {
                return;
            }

            _invulnerabilityTimer -= Time.deltaTime;

            if (_invulnerabilityTimer <= 0f)
            {
                ClearInvulnerability();
            }
        }

        private void ClearInvulnerability()
        {
            IsInvulnerable = false;
            _invulnerabilityTimer = 0f;
        }

        public void ResetPlayerState()
        {
            ClearInvulnerability();
            LivesRemaining = _startingLives;
            ActiveBombLimit = _startingActiveBombLimit;
        }

        public void RespawnPlayer()
        {
            transform.position = _startingPosition;
            SetInvulnerability(_defaultInvulnerabilityDuration);
        }

        public void SetInvulnerability(float duration)
        {
            IsInvulnerable = true;
            _invulnerabilityTimer = Mathf.Max(0f, duration);
        }

        public void LoseLife() => LivesRemaining = Mathf.Max(0, LivesRemaining - 1);
        public void AddLife() => LivesRemaining = Mathf.Min(LivesRemaining + 1, _maximumLives);
        public void IncreaseBombLimit() => ActiveBombLimit++;
    }
}
